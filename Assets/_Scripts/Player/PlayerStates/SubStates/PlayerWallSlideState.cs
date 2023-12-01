using _Scripts.Player.Data;
using _Scripts.Player.PlayerFiniteStateMachine;
using _Scripts.Player.PlayerStates.SuperStates;

namespace _Scripts.Player.PlayerStates.SubStates
{
    public class PlayerWallSlideState : PlayerTouchingWallState
    {
        public PlayerWallSlideState(PlayerFiniteStateMachine.Player player, PlayerStateMachine stateMachine, PlayerData playerData, 
            string animBoolName) : base(player, stateMachine, playerData, animBoolName) {}


        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (!IsExitingState) {
                Movement?.SetVelocityY(-PlayerData.wallSlideVelocity);

                if (GrabInput && YInput == 0) 
                    StateMachine.ChangeState(Player.WallGrabState);
            }
        }
    }
}
