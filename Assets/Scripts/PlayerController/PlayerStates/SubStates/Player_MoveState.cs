using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using PlayerController.PlayerStates.SuperStates;

namespace PlayerController.PlayerStates.SubStates
{
    public class Player_MoveState : Player_GroundedState
    {
        public Player_MoveState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
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

            player.SetVelocityX(InputX * PlayerData.movementSpeed);

            if (CrouchInput)
            {
                player.SetColliderScale(true);
                if (InputX == 0)
                {
                    stateMachine.ChangeState(player.CrouchIdleState);
                }
                else
                {
                    stateMachine.ChangeState(player.CrouchMoveState);
                }
            }
            else
            {
                if (InputX == 0)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
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
