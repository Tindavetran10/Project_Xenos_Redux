using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Weapons.Components
{
    public class PoiseDamage : WeaponComponent<PoiseDamageData, AttackPoiseDamage>
    {
        private ActionHitBox _hitBox;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IPoiseDamageable poiseDamageable))
                    poiseDamageable.DamagePoise(CurrentAttackData.Amount);
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