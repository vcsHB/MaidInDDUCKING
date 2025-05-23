using Agents.Animate;
using CameraControllers;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerIdleState : PlayerAttackableState
    {
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _canUseRope = true;
        }

        public override void Enter()
        {
            base.Enter();
            _mover.jumpCount = 2; // 나중에 스테이터스 처리
            _mover.StopImmediately();
            CameraManager.Instance.GetCompo<CameraZoomController>().SetZoomLevel(25, 1.5f, true);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            float xInput = _player.PlayerInput.InputDirection.x;
            if(Mathf.Abs(xInput) > 0)
            {
                _stateMachine.ChangeState("Move");
            }
        }

    }
}