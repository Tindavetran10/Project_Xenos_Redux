using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;

namespace _Scripts.Enemies.EnemyTypes.Ronin
{
    public class EnemyRonin_MoveState : MoveState
    {
        private readonly EnemyRonin _enemyRonin;

        public EnemyRonin_MoveState (Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_MoveState stateData, EnemyRonin enemyRonin) : base(enemy, stateMachine, animBoolName, stateData) =>
            _enemyRonin = enemyRonin;

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(IsPlayerInMinAgroRange)
                StateMachine.ChangeState(_enemyRonin.PlayerDetectedState);
            else if (IsDetectingWall || !IsDetectingLedge)
            {
                _enemyRonin.IdleState.SetFlipAfterIdle(true);
                StateMachine.ChangeState(_enemyRonin.IdleState);
            }
        }

        
    }
}