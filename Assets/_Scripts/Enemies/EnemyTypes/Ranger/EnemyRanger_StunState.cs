using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;

namespace _Scripts.Enemies.EnemyTypes.Ranger
{
    public class EnemyRanger_StunState : StunState
    {
        private readonly EnemyRanger _enemyRanger;

        public EnemyRanger_StunState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_StunState stateData, EnemyRanger enemyRanger) : base(enemy, stateMachine, animBoolName, stateData) =>
            _enemyRanger = enemyRanger;
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsStunTimeOver)
            {
                if(IsPlayerInMinAgroRange)
                    StateMachine.ChangeState(_enemyRanger.PlayerDetectedState);
                else StateMachine.ChangeState(_enemyRanger.LookForPlayerState);
            }
        }
    }
}