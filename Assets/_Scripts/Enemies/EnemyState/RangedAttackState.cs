using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState.StateData;
using _Scripts.Projectiles;
using UnityEngine;

namespace _Scripts.Enemies.EnemyState
{
    public class RangedAttackState : AttackState
    {
        private readonly D_RangedAttackState _stateData;

        private GameObject _projectile;
        private ProjectileOLD _projectileOldScript;
        
        protected RangedAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            Transform attackPosition, D_RangedAttackState stateData) : base(enemy, stateMachine, animBoolName, attackPosition) =>
            _stateData = stateData;

        public override void TriggerAttack()
        {
            base.TriggerAttack();

            _projectile = Object.Instantiate(_stateData.projectile, AttackPosition.position, AttackPosition.rotation);
            _projectileOldScript = _projectile.GetComponent<ProjectileOLD>();
            _projectileOldScript.FireProjectile(_stateData.projectileDamage ,_stateData.projectileSpeed, _stateData.projectileTravelDistance);
        }
    }
}