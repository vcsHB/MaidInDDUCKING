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
        [Tooltip("���Ƕ��̽� �׸����� �䱸�ϴ� ����")]
        public int requireMinigamePoint;

        public string reviewOnGood;
        public string reviewOnBad;
    }
}
