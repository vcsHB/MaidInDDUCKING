using UnityEngine;
using UnityEngine.UI;

namespace Basement
{
    public class TalkBubble : MonoBehaviour
    {
        public Image foodIcon;
        
        public void SetIcon(Sprite sprite)
            => foodIcon.sprite = sprite;


        public void Open()
        {
            //���߿� �ִϸ��̼� ������ �߰�����
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
