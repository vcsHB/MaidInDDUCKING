using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelectButton : MonoBehaviour, IPointerClickHandler
{
    //public event Action<CharacterType> OnClickEvent;
    //public CharacterType character;

    public void OnPointerClick(PointerEventData eventData)
    {
        //���⼭ �� �ٸ� �͵� �߰��� ���� ���� �� �� ������ ���� ���ѵ� �ǰ�
        //OnClickEvent?.Invoke(character);
    }
}
