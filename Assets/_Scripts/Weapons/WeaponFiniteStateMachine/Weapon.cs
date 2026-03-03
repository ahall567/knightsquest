using System;
using UnityEngine;
using KnightsQuest.Utils;
using KnightsQuest.CoreSystem;

namespace KnightsQuest.Weapons
{
    /// <summary>
    /// The central controller for weapon logic.
    /// Manages the combat state machine and bridges the gap between animations and data.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Time in seconds before the attack combo counter resets to zero.")]
        [SerializeField] private float attackCounterResetCooldown;

        #region State Machine References
        public WeaponStateMachine StateMachine { get; private set; }
        public WindupState WindupState { get; private set; }
        public SwingState SwingState { get; private set; }
        public RecoveryState RecoveryState { get; private set; }
        #endregion

        #region Components & Data
        public WeaponDataSO Data { get; private set; }
        public Animator Anim;
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }
        public AnimationEventHandler EventHandler { get; private set; }
        public Core Core { get; private set; }
        #endregion

        /// <summary>
        /// Tracks the current step in the attack combo.
        /// Automatically wraps back to 0 if it exceeds max attacks defined in WeaponDataSO.
        /// </summary>
        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            set => currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
        }
        private int currentAttackCounter;

        // Events for external event systems.
        public event Action OnEnter;
        public event Action OnExit;

        #region Unity Callback Functions
        private void Awake()
        {
            // Initialize references
            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;

            Anim = BaseGameObject.GetComponent<Animator>();
            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();

            // Initialize State Machine Logic
            StateMachine = new WeaponStateMachine();

            WindupState = new WindupState(this, StateMachine);
            SwingState = new SwingState(this, StateMachine);
            RecoveryState = new RecoveryState(this, StateMachine);
        }

        private void OnEnable()
        {
            // Connect Animation Events to State Transitions
            EventHandler.OnFinish += Exit;
            EventHandler.OnWindupFinish += WindupState.ExitHandler;
            EventHandler.OnSwingFinish += SwingState.ExitHandler;
            EventHandler.OnRecoveryFinish += RecoveryState.ExitHandler;
        }

        private void OnDisable()
        {
            // Clean up subscriptions to prevent memory leaks or null references
            EventHandler.OnFinish -= Exit;
            EventHandler.OnWindupFinish -= WindupState.ExitHandler;
            EventHandler.OnSwingFinish -= SwingState.ExitHandler;
            EventHandler.OnRecoveryFinish -= RecoveryState.ExitHandler;
        }
        #endregion

        /// <summary>
        /// Starts the weapon attack sequence. Triggered by the Player's Input/Combat state.
        /// </summary>
        public void Enter()
        {
            Anim.SetBool("active", true);
            Anim.SetInteger("counter", CurrentAttackCounter);

            StateMachine.Initialize(WindupState);

            OnEnter?.Invoke();
        }

        public void SetCore(Core core) => Core = core;

        public void SetData(WeaponDataSO data) => Data = data;

        /// <summary>
        /// Cleans up the weapon state and notifies the Animator/Subscribers
        /// </summary>
        public void Exit()
        {
            Anim.SetBool("active", false);
            OnExit?.Invoke();
        }

        public void ResetAttackCounter() => CurrentAttackCounter = 0;
    }
}