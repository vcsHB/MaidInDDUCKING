using System;
using Agents.Players;
using Combat;
using Combat.PlayerTagSystem;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame.GameUI.CharacterSelector
{

    public class CharacterSelectSlot : MonoBehaviour
    {
        [SerializeField] private PlayerSO _playerSO;
        [SerializeField] private Player _player;

        [SerializeField] private Image _playerIconImage;
        [SerializeField] private TextMeshProUGUI _characterNameText;
        [SerializeField] private Image _healthGauge;
        [SerializeField] private Gradient _healthFillColorLevel;
        [SerializeField] private RetireSign _retireSign;
        [Header("Select Setting")]
        [SerializeField] private float _unSelectPos;
        [SerializeField] private float _selectPos;
        [SerializeField] private float _tweenDuration;
        private RectTransform _rectTrm;

        public int PlayerId => _playerSO.id;


        private void Awake()
        {
            _rectTrm = transform as RectTransform;
        }

        private void OnDestroy()
        {
            _player.HealthCompo.OnHealthChangedValueEvent -= HandleHealthChange;
        }

        public void SetCharacterData(PlayerSO playerSO, Player player)
        {
            _playerSO = playerSO;
            _player = player;
            _playerIconImage.sprite = playerSO.characterIconSprite;

            Health ownerHealth = _player.HealthCompo;
            ownerHealth.OnHealthChangedValueEvent += HandleHealthChange;
            HandleHealthChange(ownerHealth.CurrentHealth, ownerHealth.MaxHealth);
            _characterNameText.text = playerSO.characterName;
            _player.HealthCompo.OnDieEvent.AddListener(HandleRetire);

        }


        private void HandleHealthChange(float current, float max)
        {
            float ratio = Mathf.Clamp01(current / max);
            _healthGauge.color = _healthFillColorLevel.Evaluate(ratio);
            _healthGauge.fillAmount = ratio;
        }

        private void HandleRetire()
        {
            _retireSign.SetRetire(true);
        }

        public void Select(bool value)
        {
            _rectTrm.DOAnchorPosX(value ? _selectPos : _unSelectPos, _tweenDuration);

        }

    }
}