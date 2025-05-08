using UnityEngine;
using UnityEngine.UI;

namespace Base
{
    public class BaseTalkBubble : MonoBehaviour
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
