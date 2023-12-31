using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;

namespace _Scripts.Enemies.EnemyTypes.Ronin
{
    public class EnemyRonin_IdleState : IdleState
    {
        private readonly EnemyRonin _enemyRonin;
        public EnemyRonin_IdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_IdleState stateData, EnemyRonin enemyRonin) : base(enemy, stateMachine, animBoolName, stateData) 
            => _enemyRonin = enemyRonin;

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if(IsPlayerInMinAgroRange)
                StateMachine.ChangeState(_enemyRonin.PlayerDetectedState);
            else if(IsIdleTimeOver)
                StateMachine.ChangeState(_enemyRonin.MoveState);
        }

        
    }
}