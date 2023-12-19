using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.CoreSystem.CoreComponents
{ 
    public class Combat : CoreComponent, IDamageable, IKnockBackable
    {
        private Movement _movement;
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);

        private CollisionSenses _collisionSenses;
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses 
            : Core.GetCoreComponent(ref _collisionSenses);

        private Stats _stats;
        private Stats Stats => _stats ? _stats : Core.GetCoreComponent(ref _stats);

        [SerializeField] private float maxKnockBackTime = 0.2f;

        private bool _isKnockBackActive;
        private float _knockBackStartTime;

        public override void LogicUpdate() {
            CheckKnockBack();
        }

        public void Damage(float amount) {
            Debug.Log(Core.transform.parent.name + " Damaged!");
            Stats?.DecreaseHealth(amount);
        }

        public void KnockBack(Vector2 angle, float strength, int direction)
        {
            Movement?.SetVelocity(strength, angle, direction);
            if (Movement != null) Movement.CanSetVelocity = false;
            
            _isKnockBackActive = true;
            _knockBackStartTime = Time.time;
        }

        private void CheckKnockBack() {
            if (_isKnockBackActive
                && ((Movement?.CurrentVelocity.y <= 0.01f && CollisionSenses.Ground)
                    || Time.time >= _knockBackStartTime + maxKnockBackTime)
               )
            {
                _isKnockBackActive = false;
                if (Movement != null) Movement.CanSetVelocity = true;
            }
        }

        
    }
}
