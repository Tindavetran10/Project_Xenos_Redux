using _Scripts.CoreSystem.CoreComponents;
using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState.StateData;
using UnityEngine;

namespace _Scripts.Enemies.EnemyState
{
    public class PlayerDetectedState : EnemyFiniteStateMachine.EnemyState
    {
        protected Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;

        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses 
            : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;

        private readonly D_PlayerDetectedState _stateData;

        protected bool IsPlayerInMinAgroRange;
        protected bool IsPlayerInMaxAgroRange;
        protected bool PerformLongRangeAction;
        protected bool PerformCloseRangeAction;
        protected bool IsDetectingLedge;
        
        
        protected PlayerDetectedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_PlayerDetectedState stateData) : base(enemy, stateMachine, animBoolName) =>
            _stateData = stateData;


        public override void Enter()
        {
            base.Enter();

            PerformLongRangeAction = false;
            Movement?.SetVelocityX(0f);
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Movement?.SetVelocityX(0f);

            if (Time.time >= StartTime + _stateData.longRangeActionTime)
                PerformLongRangeAction = true;
        }
        
        protected override void DoChecks()
        {
            base.DoChecks();

            IsPlayerInMinAgroRange = Enemy.CheckPlayerInMinAgroRange();
            IsPlayerInMaxAgroRange = Enemy.CheckPlayerInMaxAgroRange();
            
            IsDetectingLedge = CollisionSenses.LedgeVertical;
            PerformCloseRangeAction = Enemy.CheckPlayerInCloseRangeAction();
        }
    }
}