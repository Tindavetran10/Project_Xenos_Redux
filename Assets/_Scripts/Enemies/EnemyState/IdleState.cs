using _Scripts.CoreSystem.CoreComponents;
using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState.StateData;
using UnityEngine;

namespace _Scripts.Enemies.EnemyState
{
    public class IdleState : EnemyFiniteStateMachine.EnemyState
    {
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses 
            : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;

        private readonly D_IdleState _stateData;

        private bool _flipAfterIdle;
        protected bool IsIdleTimeOver;
        protected bool IsPlayerInMinAgroRange;

        private float _idleTime;

        protected IdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_IdleState stateData) 
            : base(enemy, stateMachine, animBoolName) =>
            _stateData = stateData;


        protected override void DoChecks() {
            base.DoChecks();
            IsPlayerInMinAgroRange = Enemy.CheckPlayerInMinAgroRange();
        }

        public override void Enter() {
            base.Enter();

            Movement?.SetVelocityX(0f);
            IsIdleTimeOver = false;
            SetRandomIdleTime();
        }

        public override void Exit() {
            base.Exit();
            if (_flipAfterIdle) Movement?.Flip();
        }

        public override void LogicUpdate() {
            base.LogicUpdate();

            Movement?.SetVelocityX(0f);
            if (Time.time >= StartTime + _idleTime) IsIdleTimeOver = true;
        }

        public void SetFlipAfterIdle(bool flip) => 
            _flipAfterIdle = flip;

        private void SetRandomIdleTime() => 
            _idleTime = Random.Range(_stateData.minIdleTime, _stateData.maxIdleTime);
    }
}