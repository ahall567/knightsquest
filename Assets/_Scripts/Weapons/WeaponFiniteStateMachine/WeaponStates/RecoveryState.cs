using UnityEngine;
namespace KnightsQuest.Weapons
{
    public class RecoveryState : WeaponState
    {

        public RecoveryState(Weapon weapon, WeaponStateMachine stateMachine) : base(weapon, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            // Increase attack counter to move to next attack in combo
            weapon.CurrentAttackCounter++;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void ExitHandler()
        {
            base.ExitHandler();

            // If animation makes it here, reset attack counter to break the combo
            weapon.ResetAttackCounter();
        }
    }
}