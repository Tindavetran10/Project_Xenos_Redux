using _Scripts.CoreSystem.CoreComponents;
using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState.StateData;

namespace _Scripts.Enemies.EnemyState
{
    public class MoveState : EnemyFiniteStateMachine.EnemyState
    {
        private Movement Movement => _movement ? _movement : Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        
        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses 
            : Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;

        protected bool IsDetectingWall;
        protected bool IsDetectingLedge;
        protected bool IsPlayerInMinAgroRange;
        
        private readonly D_MoveState _stateData;
        
        protected MoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, D_MoveState stateData) : 
            base(enemy, stateMachine, animBoolName) 
            => _stateData = stateData;

        protected override void DoChecks()
        {
            base.DoChecks();

            IsDetectingLedge = CollisionSenses.LedgeVertical;
            IsDetectingWall = CollisionSenses.WallFront;
            IsPlayerInMinAgroRange = Enemy.CheckPlayerInMinAgroRange();
        }

        public override void Enter()
        {
            base.Enter();
            Movement?.SetVelocityX(_stateData.movementSpeed * Movement.FacingDirection);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Movement?.SetVelocityX(_stateData.movementSpeed * Movement.FacingDirection);
        }
    }
}