using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;
using UnityEngine;

namespace _Scripts.Enemies.EnemyTypes.Ranger
{
    public class EnemyRanger_RangedAttackState : RangedAttackState
    {
        private readonly EnemyRanger _enemyRanger;

        public EnemyRanger_RangedAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            Transform attackPosition, D_RangedAttackState stateData, EnemyRanger enemyRanger) 
            : base(enemy, stateMachine, animBoolName, attackPosition, stateData) =>
            _enemyRanger = enemyRanger;

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (IsAnimationFinished)
            {
                if(IsPlayerInMinAgroRange)
                    StateMachine.ChangeState(_enemyRanger.PlayerDetectedState);
                else StateMachine.ChangeState(_enemyRanger.LookForPlayerState);
            }
        }
    }
}