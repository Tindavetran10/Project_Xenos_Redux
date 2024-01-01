using _Scripts.Enemies.EnemyFiniteStateMachine;
using _Scripts.Enemies.EnemyState;

namespace _Scripts.Enemies.EnemyTypes.Ronin
{
    public class EnemyRonin_DeadState : DeadState
    {
        private EnemyRonin _enemyRonin;

        public EnemyRonin_DeadState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, 
            EnemyRonin enemyRonin) : base(enemy, stateMachine, animBoolName) =>
            _enemyRonin = enemyRonin;
        
        
    }
}