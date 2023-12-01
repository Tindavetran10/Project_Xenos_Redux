using _Scripts.Player.Data;
using _Scripts.Player.PlayerFiniteStateMachine;
using _Scripts.Player.PlayerStates.SuperStates;

namespace _Scripts.Player.PlayerStates.SubStates
{
    public class PlayerJumpState : PlayerAbilityState
    {
        private int _amountOfJumpsLeft;

        public PlayerJumpState(PlayerFiniteStateMachine.Player player, PlayerStateMachine stateMachine, 
            PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) =>
            _amountOfJumpsLeft = playerData.amountOfJumps;

        public override void Enter()
        {
            base.Enter();
            Player.InputHandler.UseJumpInput();
            Movement?.SetVelocityY(PlayerData.jumpVelocity);
            IsAbilityDone = true;
            _amountOfJumpsLeft--;
            Player.InAirState.SetIsJumping();
        }
        
        public bool CanJump() => _amountOfJumpsLeft > 0;

        public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = PlayerData.amountOfJumps;
        public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
    }
}
