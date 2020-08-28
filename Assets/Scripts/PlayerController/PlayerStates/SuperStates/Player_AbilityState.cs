using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.FSM;

namespace PlayerController.PlayerStates.SuperStates
{
    public class Player_AbilityState : Player_BaseState
    {
        protected bool IsAbilityDone = false;
        protected bool IsGrounded;
        
        
        public Player_AbilityState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
        {
        }

        public override void Enter()
        {
            base.Enter();

            IsAbilityDone = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsAbilityDone)
            {
                if (IsGrounded && player.CurrentVelocity.y <= 0.01f)
                {
                    stateMachine.ChangeState(player.IdleState);
                }
                else
                {
                    stateMachine.ChangeState(player.InAirState);
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

            IsGrounded = player.CheckIfGrounded();
        }
    }
}
