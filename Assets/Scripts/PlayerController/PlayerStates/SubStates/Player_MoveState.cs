using FiniteStateMachine;
using PlayerController.Data;

namespace PlayerController.PlayerStates.SubStates
{
    public class Player_MoveState : Player_GroundedState
    {
        public Player_MoveState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerPlayerData) : base(stateMachine, animBoolName, player, playerPlayerData)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            player.CheckIfShouldFlip(InputX);
            
            player.SetVelocityX(InputX * PlayerData.movementSpeed);
            
            if (InputX == 0 && !isExitingState)
            {
                stateMachine.ChangeState(player.IdleState);
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
