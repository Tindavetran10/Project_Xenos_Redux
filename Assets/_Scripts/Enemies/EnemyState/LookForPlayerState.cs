using _Scripts.CoreSystem.CoreComponents;
using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState.StateData;
using UnityEngine;

namespace _Scripts.Enemies.EnemyState
{
    public class LookForPlayerState : EnemyFiniteStateMachine.EnemyState
    {
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        
        private bool _turnImmediately;
        private bool _isAllTurnsDone;
        protected bool IsAllTurnsTimeDone;

        private float _lastTurnTime;
        private int _amountOfTurnsDone;

        protected bool IsPlayerInMinAgroRange;

        private readonly D_LookForPlayerState _stateData;
        
        protected LookForPlayerState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_LookForPlayerState stateData) : base(enemy, stateMachine, animBoolName) =>
            _stateData = stateData;

        public override void Enter()
        {
            base.Enter();

            _isAllTurnsDone = false;
            IsAllTurnsTimeDone = false;

            _lastTurnTime = StartTime;
            _amountOfTurnsDone = 0;
            
            Movement?.SetVelocityX(0f);
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            Movement?.SetVelocityX(0f);

            if (_turnImmediately)
            {
                Movement?.Flip();
                _lastTurnTime = Time.time;
                _amountOfTurnsDone++;
                _turnImmediately = false;
            } 
            else if (Time.time >= _lastTurnTime + _stateData.timeBetweenTurns && !_isAllTurnsDone)
            {
                Movement?.Flip();
                _lastTurnTime = Time.time;
                _amountOfTurnsDone++;
            }

            if (_amountOfTurnsDone >= _stateData.amountOfTurns)
                _isAllTurnsDone = true;

            if (Time.time >= _lastTurnTime + _stateData.timeBetweenTurns && _isAllTurnsDone)
                IsAllTurnsTimeDone = true;
        }
        
        protected override void DoChecks()
        {
            base.DoChecks();
            IsPlayerInMinAgroRange = Enemy.CheckPlayerInMinAgroRange();
        }
        
        public void SetTurnImmediately(bool flip) {
            _turnImmediately = flip;
        }
    }
}