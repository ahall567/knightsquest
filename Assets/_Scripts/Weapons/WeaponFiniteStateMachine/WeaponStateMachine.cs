namespace KnightsQuest.Weapons
{
    public class WeaponStateMachine
    {
        public WeaponState CurrentState { get; private set; }

        public void Initialize(WeaponState startingState)
        {
            startingState.Enter();
            CurrentState = startingState;
        }

        public void ChangeState(WeaponState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void ExitState()
        {
            CurrentState.Exit();
        }
    }
}