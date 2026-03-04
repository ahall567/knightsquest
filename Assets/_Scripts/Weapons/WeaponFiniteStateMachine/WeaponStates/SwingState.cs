using UnityEngine;

namespace KnightsQuest.Weapons
{
    public class SwingState : WeaponState
    {

        public SwingState(Weapon weapon, WeaponStateMachine stateMachine) : base(weapon, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void ExitHandler()
        {
            base.ExitHandler();

            stateMachine.ChangeState(weapon.RecoveryState);
        }
    }
}