using _Scripts.CoreSystem.CoreComponents;
using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState.StateData;
using UnityEngine;

namespace _Scripts.Enemies.EnemyState
{
    public class DodgeState : EnemyFiniteStateMachine.EnemyState
    {
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses 
            : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;

        protected bool PerformCloseRangeAction;
        protected bool IsPlayerInMaxAgroRange;
        protected bool IsDodgeOver;
        private bool _isGrounded;

        private readonly D_DodgeState _stateData;
        
        protected DodgeState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_DodgeState stateData) 
            : base(enemy, stateMachine, animBoolName) =>
            _stateData = stateData;

        protected override void DoChecks()
        {
            base.DoChecks();

            PerformCloseRangeAction = Enemy.CheckPlayerInCloseRangeAction();
            IsPlayerInMaxAgroRange = Enemy.CheckPlayerInMaxAgroRange();
            _isGrounded = CollisionSenses.Ground;
        }

        public override void Enter()
        {
            base.Enter();

            IsDodgeOver = false;
            Movement?.SetVelocity(_stateData.dodgeSpeed, _stateData.dodgeAngle, -Movement.FacingDirection);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + _stateData.dodgeTime && _isGrounded)
                IsDodgeOver = true;
        }

    }
}