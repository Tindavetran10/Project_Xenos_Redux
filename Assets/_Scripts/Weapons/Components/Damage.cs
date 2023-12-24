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

        protected override void Start()
        {
            base.Start();
            _hitBox = GetComponent<ActionHitBox>();
            _hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        }
        

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
        }
    }
}