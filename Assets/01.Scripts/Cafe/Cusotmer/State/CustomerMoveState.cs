using Agents.Animate;
using UnityEngine;

namespace Cafe
{
    public class CustomerMoveState : CafeEntityState
    {
        private CafeEntity _entity;
        

        public CustomerMoveState(CafeEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _entity = npc;
        }

        public override void UpdateState()
        {
            float distance = Mathf.Abs(_entity.MoveTarget.position.x - _entity.transform.position.x);

            if (distance < 0.1f)
            {
                _entity.onCompleteMove?.Invoke();
                return;
            }

            float direction = Mathf.Sign(_entity.MoveTarget.position.x - _entity.transform.position.x);
            _entity.Move(direction);
        }
    }
}
