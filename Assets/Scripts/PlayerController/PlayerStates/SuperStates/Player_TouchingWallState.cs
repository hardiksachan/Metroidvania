using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;

namespace PlayerController.PlayerStates.SuperStates
{
    public class Player_TouchingWallState : Player_BaseState
    {
        protected bool IsGrounded;
        protected bool IsTouchingWall;
        protected bool GrabInput;
        
        protected int InputX;
        protected int InputY;
        
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
            InputY = player.InputHandler.NormInputY;
            GrabInput = player.InputHandler.GrabInput;

            if (IsGrounded && !GrabInput)
            {
                stateMachine.ChangeState(player.IdleState);
            } 
            else if (!IsTouchingWall || (InputX != player.FacingDirection && !GrabInput))
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
