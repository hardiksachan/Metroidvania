using FiniteStateMachine;
using PlayerController.Data;
using PlayerController.PlayerStates.SuperStates;

namespace PlayerController.PlayerStates.SubStates
{
    public class Player_JumpState : Player_AbilityState
    {
        private int _amountOfJumpsLeft;
        
        public Player_JumpState(StateMachine stateMachine, string animBoolName, Player player, PlayerData playerData) : base(stateMachine, animBoolName, player, playerData)
        {
            _amountOfJumpsLeft = playerData.amountOfJumps;
        }

        public override void Enter()
        {
            base.Enter();
            
            player.InputHandler.UseJumpInput();
            // IsAbilityDone = false;
            player.SetVelocityY(PlayerData.jumpVelocity);
            IsAbilityDone = true;
            _amountOfJumpsLeft--;
            player.InAirState.SetIsJumping();
        }

        public bool CanJump()
        {
            return _amountOfJumpsLeft > 0;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            /*if(!IsAnimationFinished) return;
            player.SetVelocityY(PlayerData.jumpVelocity);
            IsAbilityDone = true;
            _amountOfJumpsLeft--;
            player.InAirState.SetIsJumping();*/
        }

        public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = PlayerData.amountOfJumps;

        public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;    
    }
}
