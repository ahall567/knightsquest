namespace KnightsQuest.Weapons
{
    public class WindupState : WeaponState
    {

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

        public void ExitHandler()
        {
            stateMachine.ChangeState(weapon.SwingState);
        }
    }
}