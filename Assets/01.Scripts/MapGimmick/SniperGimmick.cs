using Combat.Casters;
using Combat.PlayerTagSystem;
using System.Collections;
using UnityEngine;

namespace Ingame.Gimmick
{
    public class SniperGimmick : MonoBehaviour
    {
        [Header("Speed")]
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [Space]
        [SerializeField] private float _shootDelay = 0.5f;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _blinkTime = 0.1f;
        [SerializeField] private AnimationCurve _blinkDelayCurve;
        [SerializeField] private float _originBlinkDelay;
        [Space]
        [SerializeField] private Sprite _shootSprite;
        [SerializeField] private Color _enableColor, _disableColor;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ParticleSystem _explosionParticle;
        [SerializeField] private CircleCaster _caster;
        [Space]
        [SerializeField] private Vector2 _offsetRange;

        [Header("몇초안에 방향을 따라잡을 것인지")]
        [SerializeField] private float _correction;

        private Vector2 _offset;
        private bool _isFollowingPlayer = true;
        private float _startTime;
        private float _currentSpeed;

        private bool _isBlinking = false;
        private float _prevBlink;
        private float _currentBlinkDelay;

        private float _directionProgress = 0;
        private Vector2 _targetDirection;
        private Vector2 _currentDirection;

        private Transform PlayerTrm => PlayerManager.Instance.CurrentPlayerTrm;

        private void Awake()
        {
            CalcurateOffset();
            _currentBlinkDelay = _originBlinkDelay;
            _startTime = _prevBlink = Time.time;
            _isFollowingPlayer = true;
        }

        private void Update()
        {
            if (_isFollowingPlayer)
                FindPlayer();
        }

        public void FindPlayer()
        {
            if (_startTime + _lifeTime < Time.time)
            {
                Shoot();
                _isFollowingPlayer = false;
                return;
            }

            if (_prevBlink + _currentBlinkDelay < Time.time && !_isBlinking)
            {
                float progress = (_lifeTime - (Time.time - _startTime)) / _lifeTime;
                _currentBlinkDelay = _originBlinkDelay * _blinkDelayCurve.Evaluate(progress);
                StartCoroutine("Blink");
            }

            if (Vector2.Distance(transform.position, PlayerTrm.position) < 0.1f)
            {
                CalcurateOffset();
            }

            Vector2 directionTemp = PlayerTrm.position - transform.position;
            directionTemp.Normalize();

            if (Vector2.Distance(_targetDirection, directionTemp) > 0.1f)
            {
                _directionProgress = 0;
                _currentSpeed = _minSpeed;
            }

            _targetDirection = directionTemp;
            _currentSpeed += _acceleration * Time.deltaTime;
            _currentSpeed = Mathf.Clamp(_currentSpeed, _minSpeed, _maxSpeed);

            if (_correction <= 0) _correction = 0.1f;
            _directionProgress += 1 / _correction * Time.deltaTime;
            _directionProgress = Mathf.Clamp(_directionProgress, 0, 1);

            _currentDirection = Vector2.Lerp(_currentDirection, _targetDirection, _directionProgress).normalized;
            transform.position += (Vector3)_currentDirection * _currentSpeed * Time.deltaTime;
        }

        private void Shoot()
        {
            _spriteRenderer.sprite = _shootSprite;
            StopCoroutine("Blink");
            StartCoroutine(DelayShoot());
        }

        private void CalcurateOffset()
        {
            float x = Random.Range(_offsetRange.x, _offsetRange.y);
            float y = Random.Range(_offsetRange.x, _offsetRange.y);
            if (Random.Range(0, 2) == 1) x *= -1;
            if (Random.Range(0, 2) == 1) y *= -1;

            _offset = new Vector2(x, y);
        }

        private IEnumerator DelayShoot()
        {
            yield return new WaitForSeconds(_shootDelay);
            ParticleSystem explosion = Instantiate(_explosionParticle);
            explosion.transform.position = transform.position;
            explosion.Play();
            _caster.Cast();
            Destroy(gameObject);
        }

        private IEnumerator Blink()
        {
            _isBlinking = true;
            _spriteRenderer.color = _disableColor;
            yield return new WaitForSeconds(_blinkTime);
            _isBlinking = false;
            _prevBlink = Time.time;
            _spriteRenderer.color = _enableColor;
        }
    }
}
