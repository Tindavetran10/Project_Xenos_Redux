namespace _Scripts.Player.PlayerFiniteStateMachine
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }

        // Set a current state for the player
        public void Initialize(PlayerState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        // Remove a state and add a different state to the player
        // Like idle --> move
        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}

