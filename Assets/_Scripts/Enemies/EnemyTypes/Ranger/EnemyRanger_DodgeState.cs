using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;

namespace _Scripts.Enemies.EnemyTypes.Ranger
{
    public class EnemyRanger_DodgeState : DodgeState
    {
        private readonly EnemyRanger _enemyRanger;

        public EnemyRanger_DodgeState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_DodgeState stateData, EnemyRanger enemyRanger) : base(enemy, stateMachine, animBoolName, stateData) =>
            _enemyRanger = enemyRanger;

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (IsDodgeOver)
            {
                if (IsPlayerInMaxAgroRange && !PerformCloseRangeAction)
                    StateMachine.ChangeState(_enemyRanger.RangedAttackState);
                else if (!IsPlayerInMaxAgroRange)
                    StateMachine.ChangeState(_enemyRanger.LookForPlayerState);
                //TODO: ranged attack state
            }
        }
    }
}