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

        #region State Variables
        public WeaponStateMachine StateMachine { get; private set; }
        public WindupState WindupState { get; private set; }
        public SwingState SwingState { get; private set; }
        public RecoveryState RecoveryState { get; private set; }

        #endregion

        public WeaponDataSO Data { get; private set; }

        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            // Limit currentAttackCounter to NumberOfAttacks
            set => currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
        }

        public event Action OnEnter;
        public event Action OnExit;

        public Animator Anim;

        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }
        public AnimationEventHandler EventHandler { get; private set; }

        public Core Core { get; private set; }

        private int currentAttackCounter;

        #region Unity Callback Functions
        private void Awake()
        {
            // Get game objects
            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;

            // Get components
            Anim = BaseGameObject.GetComponent<Animator>();
            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();

            // Initialize states
            StateMachine = new WeaponStateMachine();

            WindupState = new WindupState(this, StateMachine);
            SwingState = new SwingState(this, StateMachine);
            RecoveryState = new RecoveryState(this, StateMachine);
        }

        private void OnEnable()
        {
            // Subscribe to AnimationEventHandler events
            EventHandler.OnFinish += Exit;
            EventHandler.OnWindupFinish += WindupState.ExitHandler;
            EventHandler.OnSwingFinish += SwingState.ExitHandler;
            EventHandler.OnRecoveryFinish += RecoveryState.ExitHandler;
        }

        private void Update()
        {
        }

        private void OnDisable()
        {
            // Unsubscribe from AnimationEventHandlerEvents
            EventHandler.OnFinish -= Exit;
            EventHandler.OnWindupFinish -= WindupState.ExitHandler;
            EventHandler.OnSwingFinish -= SwingState.ExitHandler;
            EventHandler.OnRecoveryFinish -= Exit;
        }

        #endregion

        public void Enter()
        {
            Anim.SetBool("active", true);
            Anim.SetInteger("counter", CurrentAttackCounter);

            StateMachine.Initialize(WindupState);

            OnEnter?.Invoke();
        }

        public void SetCore(Core core)
        {
            Core = core;
        }

        public void SetData(WeaponDataSO data)
        {
            Data = data;
        }

        public void Exit()
        {
            Anim.SetBool("active", false);

            OnExit?.Invoke();
        }

        public void ResetAttackCounter() => CurrentAttackCounter = 0;
    }
}