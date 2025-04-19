using Dialog;
using System.Collections.Generic;
using UnityEngine;

namespace Cafe
{
    [CreateAssetMenu(menuName = "SO/Cafe/CustomerSO")]
    public class CafeCustomerSO : ScriptableObject
    {
        public DialogSO talk;
        public CafeCustomer customerPf;
        public float moveSpeed;

        [Tooltip("���Ƕ��̽� �׸�")]
        public List<string> miniGamePainting;

        public string GetRandomPainingName()
            => miniGamePainting[Random.Range(0, miniGamePainting.Count)];
    }
}
