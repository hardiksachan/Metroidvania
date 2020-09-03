﻿using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;
using PlayerController.PlayerStates.SuperStates;

namespace PlayerController.PlayerStates.SubStates.TouchingWall
{
    public class Player_WallClimbState : Player_TouchingWallState
    {
        public Player_WallClimbState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            player.SetVelocityY(PlayerData.wallClimbVelocity);

            if (InputY != 1)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
    }
}