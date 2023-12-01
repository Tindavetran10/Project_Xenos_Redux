using _Scripts.Player.Data;
using _Scripts.Player.PlayerFiniteStateMachine;
using _Scripts.Player.PlayerStates.SuperStates;

namespace _Scripts.Player.PlayerStates.SubStates
{
    public class PlayerWallClimbState : PlayerTouchingWallState
    {
        public PlayerWallClimbState(PlayerFiniteStateMachine.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : 
            base(player, stateMachine, playerData, animBoolName) {}


        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (!IsExitingState) {
                Movement?.SetVelocityY(PlayerData.wallClimbVelocity);

                if (YInput != 1) {
                    StateMachine.ChangeState(Player.WallGrabState);
                }
            }
        }
    }
}
