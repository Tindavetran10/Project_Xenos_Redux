using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;

namespace _Scripts.Enemies.EnemyTypes.Ronin
{
    public class EnemyRonin_LookForPlayerState : LookForPlayerState
    {
        private readonly EnemyRonin _enemyRonin;

        public EnemyRonin_LookForPlayerState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_LookForPlayerState stateData, EnemyRonin enemyRonin) : base(enemy, stateMachine, animBoolName, stateData) =>
            _enemyRonin = enemyRonin;
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if(IsPlayerInMinAgroRange)
                StateMachine.ChangeState(_enemyRonin.PlayerDetectedState);
            else if (IsAllTurnsTimeDone)
                StateMachine.ChangeState(_enemyRonin.MoveState);
        }
    }
}