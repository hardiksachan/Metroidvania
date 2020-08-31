using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;

namespace PlayerController.PlayerStates.SuperStates
{
    public class Player_OnSlopeState : Player_GroundedState
    {
        private bool _isOnSlope;
        
        public Player_OnSlopeState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
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

            if (!_isOnSlope)
            {
                if (CrouchInput)
                {
                    player.SetColliderScale(true);
                    if (InputX == 0 && stateMachine.CurrentState != player.CrouchIdleState)
                    {
                        stateMachine.ChangeState(player.CrouchIdleState);
                    }
                    else if (InputX != 0 && stateMachine.CurrentState != player.CrouchMoveState)
                    {
                        stateMachine.ChangeState(player.CrouchMoveState);
                    }
                }
                else
                {
                    player.SetColliderScale(false);
                    if (InputX == 0 && stateMachine.CurrentState != player.IdleState)
                    {
                        stateMachine.ChangeState(player.IdleState);
                    }
                    else if (InputX != 0 && stateMachine.CurrentState != player.MoveState)
                    {
                        stateMachine.ChangeState(player.MoveState);
                    }
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
            
            player.CheckSlope();
            _isOnSlope = player.IsOnSlope;
        }
    }
}
