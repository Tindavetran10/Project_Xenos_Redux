using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;


namespace _Scripts.Enemies.EnemyTypes.Ranger
{
    public class EnemyRanger_LookForPlayerState : LookForPlayerState
    {
        private readonly EnemyRanger _enemyRanger;

        public EnemyRanger_LookForPlayerState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_LookForPlayerState stateData, EnemyRanger enemyRanger) : base(enemy, stateMachine, animBoolName, stateData) =>
            _enemyRanger = enemyRanger;
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if(IsPlayerInMinAgroRange)
                StateMachine.ChangeState(_enemyRanger.PlayerDetectedState);
            else if (IsAllTurnsTimeDone)
                StateMachine.ChangeState(_enemyRanger.MoveState);
        }
    }
}