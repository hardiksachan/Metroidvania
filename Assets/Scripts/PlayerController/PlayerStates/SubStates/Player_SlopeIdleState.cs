using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using PlayerController.PlayerStates.SuperStates;
using UnityEngine;

namespace PlayerController.PlayerStates.SubStates
{
    public class Player_SlopeIdleState : Player_OnSlopeState
    {
        public Player_SlopeIdleState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
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
            
            if (InputX != 0 && !isExitingState)
            {
                stateMachine.ChangeState(player.SlopeMoveState);
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
