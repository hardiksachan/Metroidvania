using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;

namespace PlayerController.PlayerStates.SuperStates
{
    public class Player_TouchingWallState : Player_BaseState
    {
        protected bool IsGrounded;
        protected bool IsTouchingWall;
        
        protected int InputX;
        
        public Player_TouchingWallState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            InputX = player.InputHandler.NormInputX;

            if (IsGrounded)
            {
                stateMachine.ChangeState(player.IdleState);
            } else if (!IsTouchingWall || InputX != player.FacingDirection)
            {
                stateMachine.ChangeState(player.InAirState);
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

            IsGrounded = player.CheckIfGrounded();
            IsTouchingWall = player.CheckIfTouchingWall();
        }
    }
}
