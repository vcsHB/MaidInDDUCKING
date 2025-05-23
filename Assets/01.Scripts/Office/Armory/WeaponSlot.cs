using System;
using Combat.SubWeaponSystem;
using Office.InteractSystem;
using UnityEngine;
namespace Office.Armory
{
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] private SubWeaponSO _weaponSO;
        public SubWeaponSO WeaponSO => _weaponSO;
        public event Action<SubWeaponSO, WeaponSlot> OnSelectEvent;

        private InteractionTarget _interactTarget;
        private SpriteRenderer[] _visualRenderers;
        [SerializeField] private Color _activeOutlineColor;
        [SerializeField] private Color _disableOutlineColor;

        private bool _isEnable = false;
        private readonly int _disableBlinkAmountHash = Shader.PropertyToID("_Blink_amount");
        private readonly int _isOutlineBooleanHash = Shader.PropertyToID("_IsOutline");
        private readonly int _outlineColorHash = Shader.PropertyToID("_Outline_color");

        private void Awake()
        {
            _interactTarget = GetComponent<InteractionTarget>();
            _visualRenderers = GetComponentsInChildren<SpriteRenderer>();


            _interactTarget.OnHoverEnterEvent.AddListener(SetWeaponSelect);
            _interactTarget.OnHoverExitEvent.AddListener(SetWeaponUnSelect);
            _interactTarget.OnInteractEvent.AddListener(HandleInteractEvent);
        }

        public void SetEnable(bool value)
        {
            _isEnable = value;
        }

        public void SetActive(bool value)
        {
            SetWeaponSlotEnable(value);
        }


        private void SetWepaonSelect(bool value)
        {
            int outlineValue = value ? 1 : 0;
            for (int i = 0; i < _visualRenderers.Length; i++)
            {
                _visualRenderers[i].material.SetInt(_isOutlineBooleanHash, outlineValue);
            }
        }

        public void SetWeaponSelect()
        {
            if (_isEnable == false) return; 
            SetWepaonSelect(true);
        }

        public void SetWeaponUnSelect()
        {
            if (_isEnable == false) return;
            SetWepaonSelect(false);
        }

        private void SetWeaponSlotEnable(bool value)
        {
            float blinkActiveValue = value ? 0f : -1f;
            Color outlineColor = value ? _activeOutlineColor : _disableOutlineColor;
            for (int i = 0; i < _visualRenderers.Length; i++)
            {
                Material material = _visualRenderers[i].material;
                material.SetFloat(_disableBlinkAmountHash, blinkActiveValue);
                material.SetColor(_outlineColorHash, outlineColor);
            }
        }

        private void HandleInteractEvent()
        {
            if (_isEnable == false) return;

            // TODO : Send Weapon Data Later
            OnSelectEvent?.Invoke(_weaponSO, this);
        }


    }
}