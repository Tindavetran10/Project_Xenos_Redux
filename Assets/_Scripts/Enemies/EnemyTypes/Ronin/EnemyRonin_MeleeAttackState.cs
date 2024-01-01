using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;
using _Scripts.Enemies.EnemyState.StateData;
using UnityEngine;

namespace _Scripts.Enemies.EnemyTypes.Ronin
{
    public class EnemyRonin_MeleeAttackState : MeleeAttackState
    {
        private readonly EnemyRonin _enemyRonin;

        public EnemyRonin_MeleeAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            Transform attackPosition, D_MeleeAttackState stateData, EnemyRonin enemyRonin) 
            : base(enemy, stateMachine, animBoolName, attackPosition, stateData) =>
            _enemyRonin = enemyRonin;

        public override void Enter()
        {
            base.Enter();
            _comboWindow = StateData.comboTotal;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsAnimationFinished)
            {
                if(IsPlayerInMinAgroRange)
                    StateMachine.ChangeState(_enemyRonin.PlayerDetectedState);
                else StateMachine.ChangeState(_enemyRonin.LookForPlayerState);
            }
        }
    }
}