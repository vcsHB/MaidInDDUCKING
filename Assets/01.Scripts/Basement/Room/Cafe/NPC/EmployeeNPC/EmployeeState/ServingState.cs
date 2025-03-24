using Agents.Animate;
using UnityEngine;

namespace Basement.NPC
{
    public class ServingState : NPCState
    {
        private float _enterTime;
        private Employee _employee;

        public ServingState(NPC npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _employee = npc as Employee;
        }

        public override void EnterState()
        {
            base.EnterState();

            _enterTime = Time.time;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            //�ϴ� �����
            if (_enterTime + 1f < Time.time)
            {
                _employee.ServeMenu();
                stateMachine.ChangeState("Service");
            }
        }


        //�̰� ���߿� �ִϸ��̼� �߰��ϰ� �����ϴ°ɷ�
        public override void OnTriggerEnter()
        {
            base.OnTriggerEnter();
            stateMachine.ChangeState("Service");
        }
    }
}
