using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;
using UnityEngine;

namespace _Scripts.Enemies.EnemyTypes.Ranger
{
    public class EnemyRanger_PlayerDetectedState : PlayerDetectedState
    {
        private readonly EnemyRanger _enemyRanger;

        public EnemyRanger_PlayerDetectedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            D_PlayerDetectedState stateData, EnemyRanger enemyRanger) : base(enemy, stateMachine, animBoolName, stateData) =>
            _enemyRanger = enemyRanger;

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if(PerformCloseRangeAction)
            {
                if(Time.time >= _enemyRanger.DodgeState.StartTime + _enemyRanger.dodgeStateData.dodgeCooldown)
                    StateMachine.ChangeState(_enemyRanger.DodgeState);
                else StateMachine.ChangeState(_enemyRanger.RangedAttackState);
            }
            else if(PerformLongRangeAction)
                StateMachine.ChangeState(_enemyRanger.RangedAttackState);
            else if (!IsPlayerInMaxAgroRange) StateMachine.ChangeState(_enemyRanger.LookForPlayerState);
        }
    }
}