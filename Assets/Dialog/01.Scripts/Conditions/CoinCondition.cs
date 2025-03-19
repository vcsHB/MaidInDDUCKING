using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(menuName = "SO/Condition/CoinCondition")]
    public class CoinCondition : ConditionSO
    {
        [Header("�� ������ False, �� ���ų� ������ True")]
        public int coinLess;

        public override bool Decision() => DialogConditionManager.Instance.coin <= coinLess;
    }
}
