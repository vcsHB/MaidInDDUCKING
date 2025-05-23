using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerTagEnterState : PlayerState
    {
        public PlayerTagEnterState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _canUseRope = false;
        }

        public override void Enter()
        {
            if(_player.HealthCompo != null)
                _player.HealthCompo.SetResist(true);
            _player.CanCharacterChange = false;
            _player.gameObject.SetActive(true);
            base.Enter();
            _renderer.SetDissolve(true);
        }

        public override void Exit()
        {
            base.Exit();
            _player.CanCharacterChange = true;
            if(_player.HealthCompo != null)
                _player.HealthCompo.SetResist(false);
            

        }

        public override void AnimationEndTrigger()
        {
            base.AnimationEndTrigger();
            _stateMachine.ChangeState("Idle");
        }
    }
}