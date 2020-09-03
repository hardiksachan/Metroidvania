using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using PlayerController.PlayerStates.SuperStates;
using UnityEngine;

namespace PlayerController.PlayerStates.SubStates.TouchingWall
{
    public class Player_WallGrabState : Player_TouchingWallState
    {
        private Vector2 _holdPosition;
        public Player_WallGrabState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _holdPosition = player.transform.position;

            HoldPosition();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isExitingState) return;
            
            HoldPosition();
            
            if (InputY > 0)
            {
                stateMachine.ChangeState(player.WallClimbState);
            }
            else if (InputY < 0 || !GrabInput)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
        }
        
        private void HoldPosition()
        {
            player.transform.position = _holdPosition;

            player.SetVelocityX(0f);
            player.SetVelocityY(0f);
        }
    }
}
