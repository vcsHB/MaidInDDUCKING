using System;
using Combat;
using UnityEngine;
namespace Agents.Players
{
    // Aiming Data Group Structure
    public struct AimData
    {
        public bool isTargeted;
        public Vector2 mousePosition;
        public Vector2 targetPosition;
        public Vector2 originPlayerPosition;
        public float distance;
        public float distanceToPoint;
        public Vector2 aimDirection;

    }

    public struct GrabData
    {
        public bool isTargeted;
        public IGrabable grabTarget;

    }


    public class AimDetector : MonoBehaviour, IAgentComponent
    {
        public event Action<AimData> OnAimEvent;
        public event Action<GrabData> OnGrabEvent;

        [SerializeField] private float _castRadius = 0.4f;
        [SerializeField] private float _shootRadius = 12f;
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private LayerMask _targetLayer;

        private Player _player;


        // Data Properties
        private bool _isTargeted;
        private Vector2 _mousePos;
        private Vector2 _playerPos;
        private Vector2 _targetPos;
        private Vector2 _direction;

        // Grab Data
        private IGrabable _grabTarget;


        public void Initialize(Agent agent)
        {
            _player = agent as Player;
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        private void FixedUpdate()
        {
            _playerPos = _player.transform.position;
            _mousePos = _player.PlayerInput.MouseWorldPosition;
            _direction = _mousePos - (Vector2)transform.position;
            RaycastHit2D boxHit = Physics2D.CircleCast(transform.position, _castRadius, _direction, _shootRadius, _wallLayer | _targetLayer);
            _isTargeted = boxHit.collider != null;
            _grabTarget = null;
            if (_isTargeted)
            {
                _targetPos = boxHit.point;
                if (boxHit.collider.TryGetComponent(out IGrabable grabTarget))
                {
                    _targetPos = boxHit.transform.position;
                    _grabTarget = grabTarget;
                }
            }
            
            //InvokeGrabDataEvent();
            InvokeAimDataEvent();

        }

        private void InvokeAimDataEvent()
        {
            OnAimEvent?.Invoke(new AimData
            {
                mousePosition = _mousePos,
                isTargeted = _isTargeted,
                originPlayerPosition = _playerPos,
                targetPosition = _targetPos,
                aimDirection = _direction,
                distance = _direction.magnitude,
                distanceToPoint = (_targetPos - _playerPos).magnitude
            });
        }

        private void InvokeGrabDataEvent()
        {
            OnGrabEvent?.Invoke(new GrabData
            {
                isTargeted = _grabTarget != null,
                grabTarget = _grabTarget
            });
        }

    }
}