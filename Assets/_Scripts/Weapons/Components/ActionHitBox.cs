using System;
using _Scripts.CoreSystem;
using UnityEngine;

namespace _Scripts.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        public event Action<Collider2D[]> OnDetectedCollider2D;
        
        private CoreComp<CoreSystem.CoreComponents.Movement> _movement;

        private Vector2 _offset;

        private Collider2D[] _detected;
        
        private void HandleAttackAction()
        {
            var playerTransform = transform;
            var playerPosition = playerTransform.position;
            
            _offset.Set(
                playerPosition.x + CurrentAttackData.HitBox.center.x * _movement.Comp.FacingDirection,
                playerPosition.y + CurrentAttackData.HitBox.center.y);

            _detected = Physics2D.OverlapBoxAll(_offset, CurrentAttackData.HitBox.size, 0f, Data.DetectableLayers);
            
            if(_detected.Length == 0) return;
            OnDetectedCollider2D?.Invoke(_detected);
        }

        protected override void Start()
        {
            base.Start();
            _movement = new CoreComp<CoreSystem.CoreComponents.Movement>(Core);
            EventHandler.OnAttackAction += HandleAttackAction;
        }
        

        protected override void OnDestroy()
        {
            base.OnDestroy();
            EventHandler.OnAttackAction -= HandleAttackAction;
        }

        private void OnDrawGizmosSelected()
        {
            if(Data == null) return;

            foreach (var item in Data.AttackData)
            { 
                if(!item.Debug) continue;
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size); 
            }
        }
    }
}