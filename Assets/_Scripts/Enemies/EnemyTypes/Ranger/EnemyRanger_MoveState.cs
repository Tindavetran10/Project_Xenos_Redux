using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;

namespace _Scripts.Enemies.EnemyTypes.Ranger
{
    public class EnemyRanger_MoveState : MoveState
    {
        private readonly EnemyRanger _enemyRanger;

        public EnemyRanger_MoveState (Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_MoveState stateData, EnemyRanger enemyRanger) : base(enemy, stateMachine, animBoolName, stateData) =>
            _enemyRanger = enemyRanger;

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(IsPlayerInMinAgroRange)
                StateMachine.ChangeState(_enemyRanger.PlayerDetectedState);
            else if (IsDetectingWall || !IsDetectingLedge)
            {
                _enemyRanger.IdleState.SetFlipAfterIdle(true);
                StateMachine.ChangeState(_enemyRanger.IdleState);
            }
        }

        
    }
}