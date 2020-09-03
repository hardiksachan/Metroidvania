using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using PlayerController.PlayerStates.SuperStates;
using UnityEngine;

namespace PlayerController.PlayerStates.SubStates
{
    public class Player_InAirState : Player_BaseState
    {
        private int _inputX;

        private bool _jumpInput;
        private bool _jumpInputStop;
        private bool _grabInput;
        private bool _isJumping;
        private bool _isGrounded;
        private bool _coyoteTime;
        private bool _isTouchingWall;

        public Player_InAirState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
        {
            _isJumping = false;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            CheckCoyoteTime();

            _inputX = player.InputHandler.NormInputX;
            _jumpInput = player.InputHandler.JumpInput;
            _jumpInputStop = player.InputHandler.JumpInputStop;
            _grabInput = player.InputHandler.GrabInput;

            CheckJumpMultiplier();

            if (_isGrounded && player.CurrentVelocity.y <= 0.1f)
            {
                stateMachine.ChangeState(player.LandState);
            }
            else if (_jumpInput && player.JumpState.CanJump())
            {
                stateMachine.ChangeState(player.JumpState);
            }
            else if (_isTouchingWall && _grabInput)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
            else if (_isTouchingWall && _inputX == player.FacingDirection && player.CurrentVelocity.y <= 0f)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
            else
            {
                player.CheckIfShouldFlip(_inputX);
                player.SetVelocityX(PlayerData.movementSpeed * _inputX);
                
                player.Anim.SetFloat("xVel", Mathf.Abs(player.CurrentVelocity.x));
                player.Anim.SetFloat("yVel", player.CurrentVelocity.y);
            }
        }

        private void CheckCoyoteTime()
        {
            if (_coyoteTime && Time.time > startTime + PlayerData.coyoteTime)
            {
                _coyoteTime = false;
                if(!isExitingState)
                    player.JumpState.DecreaseAmountOfJumpsLeft();
            }
        }

        private void CheckJumpMultiplier()
        {
            if (_isJumping)
            {
                if (_jumpInputStop)
                {
                    player.SetVelocityY(player.CurrentVelocity.y * PlayerData.variableJumpHeightMultiplier);
                    _isJumping = false;
                }
                else if (player.CurrentVelocity.y <= 0.0f)
                {
                    _isJumping = false;
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
            _isTouchingWall = player.CheckIfTouchingWall();
        }

        public void SetIsJumping() => _isJumping = true;
        public void StartCoyoteTime() => _coyoteTime = true;
    }
}
