using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using PlayerController.PlayerStates.SuperStates;
using UnityEngine.UIElements;

namespace PlayerController.PlayerStates.SubStates
{
    public class Player_LandState : Player_GroundedState
    { 
        public Player_LandState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
        {
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
