using System;
using UnityEngine;
using KnightsQuest.Utils;
using KnightsQuest.CoreSystem;

namespace KnightsQuest.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float attackCounterResetCooldown;

        public WeaponDataSO Data { get; private set; }

        public int CurrentAttackCounter
        {
            get => currentAttackCounter;
            private set => currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
        }

        public event Action OnEnter;
        public event Action OnExit;

        private Animator anim;

        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get;  private set; }
        public AnimationEventHandler EventHandler { get; private set; }

        public Core Core { get; private set; }

        private int currentAttackCounter;

        private Timer attackCounterResetTimer;

        private void Awake()
        {
            // Get the Base game object from PrimaryWeapon
            BaseGameObject = transform.Find("Base").gameObject;
            // Get the WeaponSprite from PrimaryWeapon
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;

            anim = BaseGameObject.GetComponent<Animator>();

            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();

            attackCounterResetTimer = new Timer(attackCounterResetCooldown);
        }

        public void Enter()
        {
            print($"{transform.name} enter");

            attackCounterResetTimer.StopTimer();

            anim.SetBool("active", true);
            anim.SetInteger("counter", CurrentAttackCounter);

            ChangeState("windup");

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

        private void ChangeState(string newState)
        {
            anim.SetBool($"{newState}", true);

            Debug.Log($"{newState} enter");
        }

        private void ChangeState(string currentState, string newState)
        {
            anim.SetBool($"{currentState}", false);
            anim.SetBool($"{newState}", true);

            Debug.Log($"{newState} enter");

        }

        private void Exit()
        {
            anim.SetBool("active", false);

            CurrentAttackCounter++;
            attackCounterResetTimer.StartTimer();

            OnExit?.Invoke();
        }

        private void Update()
        {
            attackCounterResetTimer.Tick();
        }

        private void ResetAttackCounter() => CurrentAttackCounter = 0;

        private void OnEnable()
        {
            EventHandler.OnFinish += Exit;
            // EventHandler.OnRecoveryStart += StartRecovery;
            // EventHandler.OnRecoveryFinish += ResetAttackCounter;
            attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }

        private void OnDisable()
        {
            EventHandler.OnFinish -= Exit;
            // EventHandler.OnRecoveryStart -= StartRecovery;
            // EventHandler.OnRecoveryFinish -= ResetAttackCounter;
            attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
        }
    }
}