using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using PlayerController.PlayerStates.SuperStates;

namespace PlayerController.PlayerStates.SubStates
{
    public class Player_SlopeMoveState : Player_OnSlopeState
    {
        public Player_SlopeMoveState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (isExitingState) return;
            
            player.CheckIfShouldFlip(InputX);
            
            player.SetVelocity(PlayerData.movementSpeed, player.SlopeDirection * player.FacingDirection, 1);
            
            if (InputX == 0)
            {
                stateMachine.ChangeState(player.SlopeIdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }
    }
}
