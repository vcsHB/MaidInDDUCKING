using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Office.CharacterSkillTree
{
    public class CoinIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinText;
        private RectTransform RectTrm => transform as RectTransform;

        public void SetIndicator(int coin)
        {
            gameObject.SetActive(true);
            RectTrm.anchoredPosition = Mouse.current.position.ReadValue();
            _coinText.SetText($"{coin} �ʿ�");
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
