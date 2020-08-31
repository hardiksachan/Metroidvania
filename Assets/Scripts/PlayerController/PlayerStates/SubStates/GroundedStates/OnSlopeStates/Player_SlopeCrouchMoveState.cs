using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using PlayerController.PlayerStates.SuperStates;
using UnityEngine;

namespace PlayerController.PlayerStates.SubStates.GroundedStates.OnSlopeStates
{
    public class Player_SlopeCrouchMoveState : Player_OnSlopeState
    {
        public Player_SlopeCrouchMoveState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
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
            
            player.SetVelocity(PlayerData.crouchMovementSpeed, player.SlopeDirection * player.FacingDirection, 1);

            if (CrouchInput)
            {
                if (InputX == 0)
                {
                    stateMachine.ChangeState(player.SlopeCrouchIdleState);
                }
            }
            else
            {
                player.SetColliderScale(false);
                if (InputX == 0)
                {
                    stateMachine.ChangeState(player.SlopeIdleState);
                }
                else
                {
                    stateMachine.ChangeState(player.SlopeMoveState);
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
