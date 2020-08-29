using System;
using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using UnityEngine;

namespace PlayerController.PlayerStates.SuperStates
{
    public class Player_GroundedState : Player_BaseState
    {
        protected int InputX;

        protected bool isOnSlope;
        protected Vector2 slopeDir;

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
            slopeDir = player.CheckSlope();

            isOnSlope = (slopeDir.sqrMagnitude > 0) && (Math.Abs(Vector2.Dot(slopeDir, Vector2.up)) > .1f);

            if (isOnSlope)
            {
                GameObject.Find("IsOnSlope").GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                GameObject.Find("IsOnSlope").GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }
}
