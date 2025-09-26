using UnityEngine;

namespace KnightsQuest.Weapons
{
    public class WeaponState
    {
        protected Weapon weapon;
        protected WeaponStateMachine stateMachine;

        protected bool isAnimationDone;
        protected bool isExitingState;

        public WeaponState(Weapon weapon, WeaponStateMachine stateMachine)
        {
            this.weapon = weapon;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            // Debug.Log($"{animationBoolName} {weapon.CurrentAttackCounter} enter");
            isAnimationDone = false;
            isExitingState = false;
        }

        public virtual void Exit()
        {
            isExitingState = true;
        }
    }
}