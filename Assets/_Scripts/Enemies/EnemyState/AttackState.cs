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

        private int _comboCounter;
        private float _lastTimeAttack;
        private const float ComboWindow = 2;
        private static readonly int ComboCounter = Animator.StringToHash("comboCounter");

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
            
            if (_comboCounter > 2 || Time.time >= _lastTimeAttack + ComboWindow)
                _comboCounter = 0;
            
            Enemy.Anim.SetInteger(ComboCounter, _comboCounter);
            Enemy.Atsm.AttackState = this;
            IsAnimationFinished = false;
            
            // Stop the enemy moving when he's try to attack the player
            Movement?.SetVelocityX(0f);
        }

        public override void Exit()
        {
            base.Exit();
            
            _comboCounter++;
            _lastTimeAttack = Time.time;
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