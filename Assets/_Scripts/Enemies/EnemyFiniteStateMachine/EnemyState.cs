using UnityEngine;

namespace _Scripts.Enemies.EnemyFiniteStateMachine
{
    public class EnemyState
    {
        protected readonly Enemy Enemy;
        protected readonly CoreSystem.Core Core;
        protected readonly EnemyStateMachine StateMachine;


        protected float StartTime { get; private set; }

        private readonly string _animBoolName;

        // Create a constructor for enemy so we can access all the function like Update, Exit,... 
        // in different State class that inherited from EnemyState
        protected EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
        {
            Enemy = enemy;
            StateMachine = stateMachine;
            _animBoolName = animBoolName;
            Core = enemy.Core;
        }
        
        public virtual void Enter()
        {
            // Each state will have different checks
            // Like jump state will check for ground
            DoChecks();
               
            // Run the animation with the same name in the animator
            Enemy.Anim.SetBool(_animBoolName, true);
               
            // Save the time when the player enter a state 
            StartTime = Time.time;
        }
        
        public virtual void Exit() =>
            // Set the current animation to false so we can change into a new animation                                                                                                                                                                                    
            Enemy.Anim.SetBool(_animBoolName, false);

        public virtual void LogicUpdate(){}
        public virtual void PhysicsUpdate() => DoChecks();
        protected virtual void DoChecks(){}
    }
}
