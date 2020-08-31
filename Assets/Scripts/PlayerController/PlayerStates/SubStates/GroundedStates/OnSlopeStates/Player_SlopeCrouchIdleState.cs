using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using PlayerController.PlayerStates.SuperStates;
using UnityEngine;

namespace PlayerController.PlayerStates.SubStates.GroundedStates.OnSlopeStates
{
    public class Player_SlopeCrouchIdleState : Player_OnSlopeState
    {
        public Player_SlopeCrouchIdleState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            player.Rb.sharedMaterial = PlayerData.fullFrictionMat;
            
            player.SetVelocityX(0f);
            player.SetVelocityY(0f);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (CrouchInput)
            {
                if (InputX != 0)
                {
                    stateMachine.ChangeState(player.SlopeCrouchMoveState);
                }
            }
            else
            {
                player.SetColliderScale(false);
                if (InputX != 0)
                {
                    stateMachine.ChangeState(player.SlopeMoveState);
                }
                else
                {
                    stateMachine.ChangeState(player.SlopeIdleState);
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
            
            player.Rb.sharedMaterial = PlayerData.noFrictionMat;
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }
    }
}
