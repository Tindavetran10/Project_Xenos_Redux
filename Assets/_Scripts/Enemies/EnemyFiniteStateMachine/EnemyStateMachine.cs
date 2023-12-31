namespace _Scripts.Enemies.EnemyFiniteStateMachine
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentState { get; private set; }
    
        // Set a current state for the player
        public void Initialize(EnemyState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        // Remove a state and add a different state to the player
        // Like idle --> move
        public void ChangeState(EnemyState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
