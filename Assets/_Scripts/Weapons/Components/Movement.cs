using _Scripts.Weapons.Components.ComponentData;

namespace _Scripts.Weapons.Components
{
    public class Movement : WeaponComponent
    {
        private CoreSystem.CoreComponents.Movement _coreMovement;

        private CoreSystem.CoreComponents.Movement CoreMovement =>
            _coreMovement ? _coreMovement : Core.GetCoreComponent(ref _coreMovement);

        private MovementData _data;
        
        private void HandleStartMovement()
        {
            var currentAttackData = _data.AttackData[Weapon.CurrentAttackCounter];
            CoreMovement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, CoreMovement.FacingDirection);
        }

        private void HandleStopMovement() => CoreMovement.SetVelocityZero();


        protected override void Awake()
        {
            base.Awake();

            _data = Weapon.Data.GetData<MovementData>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            EventHandler.OnStartMovement += HandleStartMovement;
            EventHandler.OnStopMovement += HandleStopMovement;
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();

            EventHandler.OnStartMovement -= HandleStartMovement;
            EventHandler.OnStopMovement -= HandleStopMovement;
        }
    }
}