namespace _Scripts.Weapons.Components
{
    public class Movement : WeaponComponent<MovementData, AttackMovement>
    {
        private CoreSystem.CoreComponents.Movement _coreMovement;

        private CoreSystem.CoreComponents.Movement CoreMovement =>
            _coreMovement ? _coreMovement : Core.GetCoreComponent(ref _coreMovement);

        
        private void HandleStartMovement() => CoreMovement.SetVelocity(CurrentAttackData.Velocity, 
            CurrentAttackData.Direction, CoreMovement.FacingDirection);
        private void HandleStopMovement() => CoreMovement.SetVelocityZero();
        
        protected override void Start()
        {
            base.Start();

            EventHandler.OnStartMovement += HandleStartMovement;
            EventHandler.OnStopMovement += HandleStopMovement;
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();

            EventHandler.OnStartMovement -= HandleStartMovement;
            EventHandler.OnStopMovement -= HandleStopMovement;
        }
    }
}