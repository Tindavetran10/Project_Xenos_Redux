using _Scripts.Player.Data;
using _Scripts.Player.PlayerFiniteStateMachine;
using _Scripts.Player.PlayerStates.SuperStates;
using _Scripts.Weapons;

namespace _Scripts.Player.PlayerStates.SubStates
{
    public class PlayerHeavyAttackState : PlayerAbilityState
    {
        private readonly Weapon _weapon;
        
        private int _inputIndex;
        private bool _checkFlip;

        public PlayerHeavyAttackState(PlayerFiniteStateMachine.Player player,
            PlayerStateMachine stateMachine,
            PlayerData playerData,
            string animBoolName,
            Weapon weapon)
            : base(player, stateMachine, playerData, animBoolName)
        {
            _weapon = weapon;
            weapon.OnExit += ExitHandler;
            weapon.EventHandler.OnFlipSetActive += HandleFlipSetActive;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            var playerInputHandler = Player.InputHandler;

            var xInput = playerInputHandler.NormInputX;
            
            if(_checkFlip)
                Movement.CheckIfShouldFlip(xInput);
        }

        private void HandleFlipSetActive(bool value) => _checkFlip = value;

        public override void Enter()
        {
            base.Enter();

            _checkFlip = true;
            
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
