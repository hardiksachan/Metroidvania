﻿using FiniteStateMachine;
using PlayerController.Data;

namespace PlayerController.PlayerStates.SubStates
{
    public class Player_IdleState : Player_GroundedState
    {
        public Player_IdleState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerPlayerData) : base(stateMachine, animBoolName, player, playerPlayerData)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.SetVelocityX(0f);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (InputX != 0 && !isExitingState)
            {
                stateMachine.ChangeState(player.MoveState);
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
