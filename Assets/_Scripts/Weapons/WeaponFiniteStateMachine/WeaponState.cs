using UnityEngine;

namespace KnightsQuest.Weapons
{
    /// <summary>
    /// The base calss for all weapon states.
    /// Provides the logic for entering, exiting, and tracking state progress.
    /// </summary>
    public class WeaponState
    {
        // References to the controller and the driver
        protected Weapon weapon;
        protected WeaponStateMachine stateMachine;

        /// <summary> Set to true once the associated animation has finished playing. </summary>
        protected bool isAnimationDone;

        /// <summary>
        /// Prevents logic from running during the frame the state is transitioning.
        /// Useful for avoiding "double-triggering" logic.
        /// </summary>
        protected bool isExitingState;

        /// <summary>
        /// Constructor: Links the state to the weapon and its state machine.
        /// </summary>
        public WeaponState(Weapon weapon, WeaponStateMachine stateMachine)
        {
            this.weapon = weapon;
            this.stateMachine = stateMachine;
        }

        /// <summary>
        /// Logic that runs the moment this state becomes active.
        /// </summary>
        public virtual void Enter()
        {
            // Debug.Log($"{animationBoolName} {weapon.CurrentAttackCounter} enter");
            isAnimationDone = false;
            isExitingState = false;
        }

        /// <summary>
        /// Logic that runs just before switching to a new state.
        /// </summary>
        public virtual void Exit()
        {
            isExitingState = true;
        }

        /// <summary>
        /// Helper method usually called by the Weapon's AnimationEventHandler.
        /// </summary>
        public virtual void ExitHandler() => isAnimationDone = true;
    }
}