using System;
using NUnit.Framework.Internal;
using UnityEngine;

namespace KnightsQuest.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinish;
        public event Action OnAttackAction;
        public event Action OnWindupFinish;
        public event Action OnSwingFinish;
        public event Action OnStartMovement;
        public event Action OnStopMovement;
        public event Action OnRecoveryFinish;

        // Connects the triggers in Unity animation window to triggers in code.
        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
        private void AttackActionTrigger() => OnAttackAction?.Invoke();
        private void WindupFinishTrigger() => OnWindupFinish?.Invoke();
        private void SwingFinishTrigger() => OnSwingFinish?.Invoke();
        private void StartMovementTrigger() => OnStartMovement?.Invoke();
        private void StopMovementTrigger() => OnStopMovement?.Invoke();
        private void RecoveryFinishTrigger() => OnRecoveryFinish?.Invoke();
    }
}
