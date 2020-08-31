using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;

namespace PlayerController.PlayerStates.SuperStates
{
    public class Player_GroundedState : Player_BaseState
    {
        protected int InputX;

        protected bool CrouchInput;
        
        private bool _jumpInput;
        private bool _grabInput;
        private bool _isGrounded;
        private bool _isTouchingWall;
        private bool _isOnSlope;
    
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
            CrouchInput = player.InputHandler.CrouchInput;

            if (_jumpInput && player.JumpState.CanJump())
            {
                stateMachine.ChangeState(player.JumpState);
            }
            else if (!_isGrounded)
            {
                player.InAirState.StartCoyoteTime();
                stateMachine.ChangeState(player.InAirState);
            }
            else if (_isOnSlope)
            {
                if (CrouchInput)
                {
                    player.SetColliderScale(true);
                    if (InputX == 0 && stateMachine.CurrentState != player.SlopeCrouchIdleState)
                    {
                        stateMachine.ChangeState(player.SlopeCrouchIdleState);
                    }
                    else if (InputX != 0 && stateMachine.CurrentState != player.SlopeCrouchMoveState)
                    {
                        stateMachine.ChangeState(player.SlopeCrouchMoveState);
                    }
                }
                else
                {
                    player.SetColliderScale(false);
                    if (InputX == 0 && stateMachine.CurrentState != player.SlopeIdleState)
                    {
                        stateMachine.ChangeState(player.SlopeIdleState);
                    }
                    else if (InputX != 0 && stateMachine.CurrentState != player.SlopeMoveState)
                    {
                        stateMachine.ChangeState(player.SlopeMoveState);
                    }
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

            _isGrounded = player.CheckIfGrounded();
            player.CheckSlope();
            _isOnSlope = player.IsOnSlope;
        }
    }
}
