using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using PlayerController.PlayerStates.SuperStates;
using UnityEngine;

namespace PlayerController.PlayerStates.SubStates
{
    public class Player_CrouchMoveState : Player_GroundedState
    {
        public Player_CrouchMoveState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
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
            
            player.SetVelocityX(InputX * PlayerData.crouchMovementSpeed);

            if (CrouchInput)
            {
                if (InputX == 0)
                {
                    stateMachine.ChangeState(player.CrouchIdleState);
                }
            }
            else
            {
                player.SetColliderScale(false);
                if (InputX == 0)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
                else
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
