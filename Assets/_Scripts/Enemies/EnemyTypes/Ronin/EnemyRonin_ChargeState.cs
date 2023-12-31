using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;

namespace _Scripts.Enemies.EnemyTypes.Ronin
{
    public class EnemyRonin_ChargeState : ChargeState
    {
        private readonly EnemyRonin _enemyRonin;

        public EnemyRonin_ChargeState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_ChargeState stateData, EnemyRonin enemyRonin) : base(enemy, stateMachine, animBoolName, stateData) =>
            _enemyRonin = enemyRonin;
        

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if(PerformCloseRangeAction)
                StateMachine.ChangeState(_enemyRonin.MeleeAttackState);
            else if(!IsDetectingLedge || IsDetectingWall)
                StateMachine.ChangeState(_enemyRonin.LookForPlayerState);
            else if (IsChargeTimeOver)
            {
                if(IsPlayerInMinAgroRange)
                    StateMachine.ChangeState(_enemyRonin.PlayerDetectedState);
                else StateMachine.ChangeState(_enemyRonin.LookForPlayerState);
            }
                
        }
    }
}