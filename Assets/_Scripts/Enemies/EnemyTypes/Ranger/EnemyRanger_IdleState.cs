using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;

namespace _Scripts.Enemies.EnemyTypes.Ranger
{
    public class EnemyRanger_IdleState : IdleState
    {
        private readonly EnemyRanger _enemyRanger;
        public EnemyRanger_IdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_IdleState stateData, EnemyRanger enemyRanger) : base(enemy, stateMachine, animBoolName, stateData) 
            => _enemyRanger = enemyRanger;

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if(IsPlayerInMinAgroRange)
                StateMachine.ChangeState(_enemyRanger.PlayerDetectedState);
            else if(IsIdleTimeOver)
                StateMachine.ChangeState(_enemyRanger.MoveState);
        }

        
    }
}