using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;

namespace _Scripts.Enemies.EnemyTypes.Ronin
{
    public class EnemyRonin_StunState : StunState
    {
        private readonly EnemyRonin _enemyRonin;

        public EnemyRonin_StunState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_StunState stateData, EnemyRonin enemyRonin) : base(enemy, stateMachine, animBoolName, stateData) =>
            _enemyRonin = enemyRonin;
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsStunTimeOver)
            {
                if(PerformCloseRangeAction)
                    StateMachine.ChangeState(_enemyRonin.MeleeAttackState);
                else if(IsPlayerInMinAgroRange)
                    StateMachine.ChangeState(_enemyRonin.ChargeState);
                else
                {
                    _enemyRonin.LookForPlayerState.SetTurnImmediately(true);
                    StateMachine.ChangeState(_enemyRonin.LookForPlayerState);
                }
            }
        }
    }
}