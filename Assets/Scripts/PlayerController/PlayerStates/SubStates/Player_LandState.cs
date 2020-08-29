using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.PlayerStates.SuperStates;
using UnityEngine.UIElements;

namespace PlayerController.PlayerStates.SubStates
{
    public class Player_LandState : Player_GroundedState
    { 
        public Player_LandState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            if (isOnSlope) {
                player.Rb.sharedMaterial = PlayerData.fullFrictionMat;
            }
        }
        

        public override void Exit()
        {
            base.Exit();

            if (player.Rb.sharedMaterial.friction > 10f)
            {
                player.Rb.sharedMaterial = PlayerData.noFrictionMat;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!isExitingState)
            {
                if (InputX != 0)
                {
                    stateMachine.ChangeState(player.MoveState);
                } else if (IsAnimationFinished)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
            }
        }
    }
    
}
