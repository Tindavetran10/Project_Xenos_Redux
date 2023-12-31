using _Scripts.CoreSystem.CoreComponents;
using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState.StateData;
using UnityEngine;

namespace _Scripts.Enemies.EnemyState
{
    public class AttackState : EnemyFiniteStateMachine.EnemyState
    {
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        
        protected readonly Transform AttackPosition;

        protected bool IsAnimationFinished;
        protected bool IsPlayerInMinAgroRange;

        private D_MeleeAttackState _state;

        protected AttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName,
            Transform attackPosition) : base(enemy, stateMachine, animBoolName)
        {
            AttackPosition = attackPosition;
            _movement = Core.GetCoreComponent<Movement>();
        }

        public override void Enter()
        {
            base.Enter();
            Enemy.Atsm.AttackState = this;
            IsAnimationFinished = false;
            
            // Stop the enemy moving when he's try to attack the player
            Movement?.SetVelocityX(0f);
        }
        
        public virtual void TriggerAttack() {}

        public virtual void FinishAttack() => IsAnimationFinished = true;

        protected override void DoChecks()
        {
            base.DoChecks();
            IsPlayerInMinAgroRange = Enemy.CheckPlayerInMinAgroRange();
        }
    }
}