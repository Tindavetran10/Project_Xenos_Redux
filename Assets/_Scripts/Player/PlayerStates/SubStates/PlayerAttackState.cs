using _Scripts.Player.Data;
using _Scripts.Player.PlayerFiniteStateMachine;
using _Scripts.Player.PlayerStates.SuperStates;
using _Scripts.Weapons;

namespace _Scripts.Player.PlayerStates.SubStates
{
    public class PlayerAttackState : PlayerAbilityState
    {
        private readonly Weapon _weapon;

        public PlayerAttackState(
            PlayerFiniteStateMachine.Player player,
            PlayerStateMachine stateMachine,
            PlayerData playerData,
            string animBoolName,
            Weapon weapon)
            : base(player, stateMachine, playerData, animBoolName)
        {
            _weapon = weapon;
            weapon.OnExit += ExitHandler;
        }

        public override void Enter()
        {
            base.Enter();
            // Call the enter function from Weapon class
            _weapon.Enter();
        }

        private void ExitHandler()
        {
            AnimationFinishTrigger();
            IsAbilityDone = true;
        }
    }
}
