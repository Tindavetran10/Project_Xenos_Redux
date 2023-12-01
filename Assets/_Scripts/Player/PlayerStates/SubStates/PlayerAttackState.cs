using _Scripts.Player.Data;
using _Scripts.Player.PlayerFiniteStateMachine;
using _Scripts.Player.PlayerStates.SuperStates;
using _Scripts.Weapons;

namespace _Scripts.Player.PlayerStates.SubStates
{
    public class PlayerAttackState : PlayerAbilityState
    {
        private Weapon _weapon;

        public PlayerAttackState(
            PlayerFiniteStateMachine.Player player,
            PlayerStateMachine stateMachine,
            PlayerData playerData,
            string animBoolName,
            Weapon weapon)
            : base(player, stateMachine, playerData, animBoolName)
        {
            _weapon = weapon;
        }

        public override void Enter()
        {
            base.Enter();
            
            _weapon.Enter();
        }
    }
}
