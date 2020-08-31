using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using PlayerController.PlayerStates.SuperStates;

namespace PlayerController.PlayerStates.SubStates.GroundedStates
{
    public class Player_IdleState : Player_GroundedState
    {
        public Player_IdleState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.SetVelocityX(0f);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (isExitingState) return;

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
                if (InputX != 0)
                {
                    stateMachine.ChangeState(player.MoveState);
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
