using _Scripts.CoreSystem.CoreComponents;
using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState.StateData;
using _Scripts.Interfaces;
using UnityEngine;

namespace _Scripts.Enemies.EnemyState
{
    public class MeleeAttackState : AttackState
    {
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;

        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses 
            : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;

        private readonly D_MeleeAttackState _stateData;
        
        protected MeleeAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            Transform attackPosition, D_MeleeAttackState stateData) : base(enemy, stateMachine, animBoolName, attackPosition) =>
            _stateData = stateData;

        public override void TriggerAttack()
        {
            base.TriggerAttack();
            var detectedObjects = Physics2D.OverlapCircleAll(AttackPosition.position, 
                _stateData.attackRadius, _stateData.whatIsPlayer);

            foreach (var collider in detectedObjects)
            {
                var damageable = collider.GetComponent<IDamageable>();
                damageable?.Damage(_stateData.attackDamage);

                var knockBackable = collider.GetComponent<IKnockBackable>();
                if (knockBackable != null) {
                    knockBackable.KnockBack(_stateData.knockbackAngle, _stateData.knockbackStrength, Movement.FacingDirection);
                }
            }
        }
    }
}