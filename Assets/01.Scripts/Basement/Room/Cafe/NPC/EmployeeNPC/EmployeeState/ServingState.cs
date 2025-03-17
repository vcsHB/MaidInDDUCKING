using Agents.Animate;
using UnityEngine;

namespace Basement.NPC
{
    public class ServingState : NPCState
    {
        private float _enterTime;

        public ServingState(NPC npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {

        }

        public override void EnterState()
        {
            base.EnterState();

            _enterTime = Time.time;

        }

        public override void UpdateState()
        {
            base.UpdateState();


        }


        //�̰� ���߿� �ִϸ��̼� �߰��ϰ� �����ϴ°ɷ�
        public override void OnTriggerEnter()
        {
            base.OnTriggerEnter();
            stateMachine.ChangeState("Service");
        }
    }
}
