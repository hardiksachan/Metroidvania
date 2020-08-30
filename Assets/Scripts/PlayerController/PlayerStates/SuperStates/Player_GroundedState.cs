using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;

namespace PlayerController.PlayerStates.SuperStates
{
    public class Player_GroundedState : Player_BaseState
    {
        protected int InputX;

        private bool _jumpInput;
        private bool _grabInput;
        private bool _isGrounded;
        private bool _isTouchingWall;
    
        public Player_GroundedState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            player.JumpState.ResetAmountOfJumpsLeft();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            InputX = player.InputHandler.NormInputX;
            _jumpInput = player.InputHandler.JumpInput;

            if (_jumpInput && player.JumpState.CanJump())
            {
                stateMachine.ChangeState(player.JumpState);
            } else if (!_isGrounded)
            {
                player.InAirState.StartCoyoteTime();
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

            _isGrounded = player.CheckIfGrounded();
        }
    }
}
