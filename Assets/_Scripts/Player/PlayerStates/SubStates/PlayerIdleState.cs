using _Scripts.Player.Data;
using _Scripts.Player.PlayerFiniteStateMachine;
using _Scripts.Player.PlayerStates.SuperStates;

namespace _Scripts.Player.PlayerStates.SubStates
{
    public class PlayerIdleState : PlayerGroundedState {
        public PlayerIdleState(PlayerFiniteStateMachine.Player player, PlayerStateMachine stateMachine, 
            PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) {}
        
        public override void Enter() {
            base.Enter();
            Movement?.SetVelocityX(0f);
        }

        public override void LogicUpdate() {
            base.LogicUpdate();

            if (!IsExitingState)
            {
                if (XInput != 0)
                    StateMachine.ChangeState(Player.MoveState);
                else if (YInput == -1)
                    StateMachine.ChangeState(Player.CrouchIdleState);
            }
        }
    }
}