using System;
using UnityEngine;

namespace KnightsQuest.Weapons
{
    /// <summary>
    /// Acts as a relay between Unity's Animation Events and the Weapon State Machine.
    /// Note: Method names here MUST match the "Function" name defined in the Unity Animation window.
    /// </summary>
    public class AnimationEventHandler : MonoBehaviour
    {
        #region Events
        public event Action OnFinish;
        public event Action OnAttackAction;
        public event Action OnWindupFinish;
        public event Action OnSwingFinish;
        public event Action OnStartMovement;
        public event Action OnStopMovement;
        public event Action OnRecoveryFinish;
        #endregion

        // --- Trigger Methods called by Unity Animations ---

        /// <summary> Called at the very end of the animation clip. </summary>
        private void AnimationFinishedTrigger() => OnFinish?.Invoke();

        /// <summary> Called at the exact frame the weapon should deal damage. </summary>
        private void AttackActionTrigger() => OnAttackAction?.Invoke();

        /// <summary> Signals the end of the Windup phase; usually transitions to Swing. </summary>
        private void WindupFinishTrigger() => OnWindupFinish?.Invoke();

        /// <summary> Signals the end of the Swing phase; usually transitions to Recovery. </summary>
        private void SwingFinishTrigger() => OnSwingFinish?.Invoke();

        /// <summary> Signals the beginning of the attack movement </summary>
        private void StartMovementTrigger() => OnStartMovement?.Invoke();

        /// <summary> Signals the end of the attack movement </summary>
        private void StopMovementTrigger() => OnStopMovement?.Invoke();

        /// <summary> Signals the end of the Recovery phase; allows for combo chaining or exiting. </summary>
        private void RecoveryFinishTrigger() => OnRecoveryFinish?.Invoke();
    }
}
