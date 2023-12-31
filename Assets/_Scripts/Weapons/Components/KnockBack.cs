using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Weapons.Components
{
    // Inherit weapon component with 2 types of data parameter
    public class KnockBack : WeaponComponent<KnockBackData, AttackKnockBack>
    {
        private ActionHitBox _hitBox;

        private CoreSystem.CoreComponents.Movement _movement;
        
        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IKnockBackable knockBackable)) 
                    knockBackable.KnockBack(CurrentAttackData.Angle, CurrentAttackData.Strength, _movement.FacingDirection);
            }
        }

        protected override void Start()
        {
            base.Start();

            _hitBox = GetComponent<ActionHitBox>();
            _hitBox.OnDetectedCollider2D += HandleDetectCollider2D;

            _movement = Core.GetCoreComponent<CoreSystem.CoreComponents.Movement>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }
    }
}