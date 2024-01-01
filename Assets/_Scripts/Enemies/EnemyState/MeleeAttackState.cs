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

        protected readonly D_MeleeAttackState StateData;
        
        protected MeleeAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            Transform attackPosition, D_MeleeAttackState stateData) : base(enemy, stateMachine, animBoolName, attackPosition) =>
            StateData = stateData;
        

        public override void TriggerAttack()
        {
            base.TriggerAttack();
            var detectedObjects = Physics2D.OverlapCircleAll(AttackPosition.position, 
                StateData.attackRadius, StateData.whatIsPlayer);

            foreach (var collider in detectedObjects)
            {
                var damageable = collider.GetComponent<IDamageable>();
                damageable?.Damage(StateData.attackDamage);

                var knockBackable = collider.GetComponent<IKnockBackable>();
                
                if (knockBackable != null) {
                    knockBackable.KnockBack(StateData.knockbackAngle, StateData.knockbackStrength, Movement.FacingDirection);
                }

                var poiseDamageable = collider.GetComponent<IPoiseDamageable>();
                if(poiseDamageable != null)
                    poiseDamageable.DamagePoise(StateData.poiseDamage);
            }
        }
    }
}