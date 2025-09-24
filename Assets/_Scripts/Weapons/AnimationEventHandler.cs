using System;
using UnityEngine;

namespace KnightsQuest.Weapons
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinish;
        public event Action OnStartMovement;
        public event Action OnStopMovement;
        public event Action OnAttackAction;
        public event Action OnRecoveryStart;
        public event Action OnRecoveryFinish;

        // Invokes Actions on Triggers
        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
        private void StartMovementTrigger() => OnStartMovement?.Invoke();
        private void StopMovementTrigger() => OnStopMovement?.Invoke();
        private void AttackActionTrigger() => OnAttackAction?.Invoke();
        private void RecoveryTrigger() => OnRecoveryStart?.Invoke();
        private void RecoveryFinishTrigger() => OnRecoveryFinish?.Invoke();
    }
}
