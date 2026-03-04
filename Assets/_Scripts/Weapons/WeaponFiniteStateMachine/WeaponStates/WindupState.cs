namespace KnightsQuest.Weapons
{
    /// <summary>
    /// Handles the "anticipation" phase of an attack.
    /// This state stays active until the AnimationEventHandler triggers ExitHandler.
    /// </summary>
    public class WindupState : WeaponState
    {
        /// <summary>
        /// Constructor: Passes references up to the base WeaponState.
        /// </summary>
        public WindupState(Weapon weapon, WeaponStateMachine stateMachine) : base(weapon, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        /// <summary>
        /// Triggered by AnimationEventHandler's OnWindupFinish event.
        /// Automatically moves the weapon from the Windup phase into the active Swing phase.
        /// </summary>
        public override void ExitHandler()
        {
            base.ExitHandler();

            stateMachine.ChangeState(weapon.SwingState);
        }
    }
}