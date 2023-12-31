using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.CoreSystem.CoreComponents
{ 
    public class KnockBackReceiver : CoreComponent,  IKnockBackable
    {
        [SerializeField] private float maxKnockBackTime = 0.2f;

        private bool _isKnockBackActive;
        private float _knockBackStartTime;

        private CoreComp<Movement> _movement;
        private CoreComp<CollisionSenses> _collisionSenses;

        public override void LogicUpdate() => CheckKnockBack();


        public void KnockBack(Vector2 angle, float strength, int direction)
        {
            _movement.Comp?.SetVelocity(strength, angle, direction);
            _movement.Comp.CanSetVelocity = false;
            
            _isKnockBackActive = true;
            _knockBackStartTime = Time.time;
        }

        private void CheckKnockBack() {
            if (_isKnockBackActive
                && ((_movement.Comp?.CurrentVelocity.y <= 0.01f && _collisionSenses.Comp.Ground)
                    || Time.time >= _knockBackStartTime + maxKnockBackTime)
               )
            {
                _isKnockBackActive = false;
                _movement.Comp.CanSetVelocity = true;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            _movement = new CoreComp<Movement>(Core);
            _collisionSenses = new CoreComp<CollisionSenses>(Core);
        }
    }
}
