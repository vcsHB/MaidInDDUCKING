using InputManage;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Office
{
    public class CharacterFormationSlot : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public UIInputReader input;
        public TextMeshProUGUI nameText;
        public Image icon;
        public CharacterIconSO iconSO;
        public CharacterEnum characterType;
        public int index;

        private CharacterFormation _formation;
        private Vector2 _offset;

        public RectTransform RectTransform => transform as RectTransform;
        public Vector2 screenOffset => new Vector2(Screen.width / 2, Screen.height / 2);


        public void Init(CharacterFormation characterFormation)
        {
            _formation = characterFormation;
            nameText.SetText(characterType.ToString());
            icon.sprite = iconSO.GetIcon(characterType);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _offset = (input.MousePositionOnScreen - screenOffset) - RectTransform.anchoredPosition;
            transform.SetAsLastSibling();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _formation.SetFormationSlotPosition();
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransform.anchoredPosition = (input.MousePositionOnScreen - screenOffset) - _offset;
            _formation.OnDrag(this);
        }
    }
}
