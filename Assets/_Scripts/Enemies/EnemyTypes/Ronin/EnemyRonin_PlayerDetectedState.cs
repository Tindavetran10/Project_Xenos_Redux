using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;

namespace _Scripts.Enemies.EnemyTypes.Ronin
{
    public class EnemyRonin_PlayerDetectedState : PlayerDetectedState
    {
        private readonly EnemyRonin _enemyRonin;

        public EnemyRonin_PlayerDetectedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_PlayerDetectedState stateData, EnemyRonin enemyRonin) : base(enemy, stateMachine, animBoolName, stateData) =>
            _enemyRonin = enemyRonin;

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if(PerformCloseRangeAction)
                StateMachine.ChangeState(_enemyRonin.MeleeAttackState);
            else if(PerformLongRangeAction)
                StateMachine.ChangeState(_enemyRonin.ChargeState);
            else if(!IsPlayerInMaxAgroRange)
                StateMachine.ChangeState(_enemyRonin.LookForPlayerState);
            else if (!IsDetectingLedge)
            {
                Movement?.Flip();
                StateMachine.ChangeState(_enemyRonin.MoveState);
            }
        }
    }
}