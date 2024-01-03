using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;

namespace _Scripts.Enemies.EnemyTypes.Ranger
{
    public class EnemyRanger_DeadState : DeadState
    {
        private readonly EnemyRanger _enemyRanger;

        public EnemyRanger_DeadState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            EnemyRanger enemyRanger) : base(enemy, stateMachine, animBoolName) =>
            _enemyRanger = enemyRanger;

        public override void Enter()
        {
            base.Enter();
            _enemyRanger.KillEnemy();
        }
    }
}