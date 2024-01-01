using _Scripts.CoreSystem.CoreComponents;
using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState.StateData;
using UnityEngine;

namespace _Scripts.Enemies.EnemyState
{
    public class StunState : EnemyFiniteStateMachine.EnemyState
    {
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses 
            : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;

        protected bool IsStunTimeOver;
        private bool _isGrounded;
        private bool _isMovementStopped;
        protected bool PerformCloseRangeAction;
        protected bool IsPlayerInMinAgroRange;

        private readonly D_StunState _stateData;
        
        protected StunState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_StunState stateData) 
            : base(enemy, stateMachine, animBoolName) =>
            _stateData = stateData;

        public override void Enter()
        {
            base.Enter();

            IsStunTimeOver = false;
            _isMovementStopped = false;
            Movement?.SetVelocity(_stateData.stunKnockbackSpeed, _stateData.stunKnockbackAngle, Enemy.LastDamageDirection);
        }

        public override void Exit()
        {
            base.Exit();
            Enemy.ResetStunResistance();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + _stateData.stunTime)
                IsStunTimeOver = true;

            if (_isGrounded && Time.time >= StartTime + _stateData.stunKnockbackTime && !_isMovementStopped)
            {
                _isMovementStopped = true;
                Movement?.SetVelocityX(0f);
            }
        }

        protected override void DoChecks()
        {
            base.DoChecks();
            
            _isGrounded = CollisionSenses.Ground;
            PerformCloseRangeAction = Enemy.CheckPlayerInCloseRangeAction();
            IsPlayerInMinAgroRange = Enemy.CheckPlayerInMinAgroRange();
        }
    }
}