using _Scripts.Enemies.EnemyFiniteStateMachine;

namespace _Scripts.Enemies.EnemyState
{
    public class DeadState : EnemyFiniteStateMachine.EnemyState
    {
        protected DeadState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Enemy.gameObject.SetActive(false);
        }
    }
}