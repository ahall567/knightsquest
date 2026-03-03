namespace KnightsQuest.Weapons
{
    /// <summary>
    /// Handles the transition between different weapon states (Windup, Swing, Recovery).
    /// Ensures thast only one state is active at a time and manages the Enter/Exit lifecycle.
    /// </summary>
    public class WeaponStateMachine
    {

        /// <summary> The state the weapon is currently executing </summary>
        public WeaponState CurrentState { get; private set; }


        /// <summary>
        /// Sets the initial state of the weapon.
        /// Should be called when an attack first starts.
        /// </summary>
        /// <param name="startingState">The first state to enter (usually WindupState).</param>
        public void Initialize(WeaponState startingState)
        {
            startingState.Enter();
            CurrentState = startingState;
        }

        /// <summary>
        /// Exits the current state and enters a new one.
        /// Use this to move through the combat sequence (e.g., Windup -> Swing).
        /// </summary>
        /// <param name="newState">The state to transition to.</param>
        public void ChangeState(WeaponState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        /// <summary>
        /// Terminates the current state logice without entering a new one.
        /// Useful for stopping an attack early or clearing the machine.
        /// </summary>
        public void ExitState()
        {
            CurrentState?.Exit();
            CurrentState = null; // Clear the state so we don't accidentally run logic on it later.
        }
    }
}