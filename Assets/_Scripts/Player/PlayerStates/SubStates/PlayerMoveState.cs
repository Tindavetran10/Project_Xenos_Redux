using _Scripts.Player.Data;
using _Scripts.Player.PlayerFiniteStateMachine;
using _Scripts.Player.PlayerStates.SuperStates;

namespace _Scripts.Player.PlayerStates.SubStates
{
    public class PlayerMoveState : PlayerGroundedState
    {
        // This is the Move State, which will change the x axis of player base on the input
        public PlayerMoveState(PlayerFiniteStateMachine.Player player, PlayerStateMachine stateMachine, 
            PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {} 
    
        public override void LogicUpdate() {
            base.LogicUpdate();

            // Check if we should flip the player or not, base on the xInput
            Movement?.CheckIfShouldFlip(XInput);
            // Apply a new velocity to player 
            Movement?.SetVelocityXSmoothDamp(PlayerData.movementVelocity * XInput);

            if (IsExitingState) return;
            // Change the player to Idle State when the input is 0
            if (XInput == 0)
                StateMachine.ChangeState(Player.IdleState);
            // Change to Crouch State if the y Input is -1
            else if (YInput == -1)
                StateMachine.ChangeState(Player.CrouchMoveState);
        }
    }
}
