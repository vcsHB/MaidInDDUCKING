using System.Collections.Generic;
using UnityEngine;

namespace Cafe
{
    [CreateAssetMenu(menuName = "SO/Cafe/CustomerSO")]
    public class CafeCustomerSO : ScriptableObject
    {
        public CafeCustomer customerPf;
        public Sprite customerIcon;
        public float moveSpeed;
        public float menuWaitingTime;

        [Tooltip("���Ƕ��̽� �׸� �䱸�� Ȯ�� 100�з�")]
        public int minigameRequireChance;
        [Tooltip("���Ƕ��̽� �׸�")]
        public List<string> miniGamePainting;

        public string reviewOnGood;
        public string reviewOnBad;

        public string GetRandomPainingName()
            => miniGamePainting[Random.Range(0, miniGamePainting.Count)];
    }
}
