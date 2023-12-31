using _Scripts.CoreSystem.CoreComponents;
using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState.StateData;
using UnityEngine;

namespace _Scripts.Enemies.EnemyState
{
    public class ChargeState : EnemyFiniteStateMachine.EnemyState
    {
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses 
            : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;

        private readonly D_ChargeState _stateData;
        
        protected bool IsPlayerInMinAgroRange;
        protected bool IsDetectingLedge;
        protected bool IsDetectingWall;
        protected bool IsChargeTimeOver;
        protected bool PerformCloseRangeAction;
        
        protected ChargeState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_ChargeState stateData) 
            : base(enemy, stateMachine, animBoolName) =>
            _stateData = stateData;

        public override void Enter()
        {
            base.Enter();

            IsChargeTimeOver = false;
            Movement?.SetVelocityX(_stateData.chargeSpeed * Movement.FacingDirection);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            Movement?.SetVelocityX(_stateData.chargeSpeed * Movement.FacingDirection);

            if (Time.time >= StartTime + _stateData.chargeTime)
                IsChargeTimeOver = true;
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            IsPlayerInMinAgroRange = Enemy.CheckPlayerInMinAgroRange();
            IsDetectingLedge = CollisionSenses.LedgeVertical;
            IsDetectingWall = CollisionSenses.WallFront;

            PerformCloseRangeAction = Enemy.CheckPlayerInCloseRangeAction();
        }
    }
}