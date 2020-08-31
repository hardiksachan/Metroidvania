using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using PlayerController.PlayerStates.SuperStates;

namespace PlayerController.PlayerStates.SubStates.GroundedStates
{
    public class Player_CrouchIdleState : Player_GroundedState
    {
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
                if (InputX != 0)
                {
                    stateMachine.ChangeState(player.CrouchMoveState);
                }
            }
            else
            {
                player.SetColliderScale(false);
                if (InputX != 0)
                {
                    stateMachine.ChangeState(player.MoveState);
                }
                else
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

        public Player_CrouchIdleState(StateMachine stateMachine, string animBoolName, Player player,
            PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
        {
        }
    }
}