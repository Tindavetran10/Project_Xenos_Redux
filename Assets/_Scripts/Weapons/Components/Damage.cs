using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Weapons.Components
{
    public class Damage : WeaponComponent<DamageData, AttackDamage>
    {
        private ActionHitBox _hitBox;
        
        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IDamageable damageable)) 
                    damageable.Damage(CurrentAttackData.Amount);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _hitBox = GetComponent<ActionHitBox>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }
    }
}