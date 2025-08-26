using KnightsQuest.CoreSystem;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;

        // TODO: Fix this when finishing weapon data
        // protected AnimationEventHandler EventHandler => weapon.EventHandler;
        protected AnimationEventHandler eventHandler;
        protected Core Core => weapon.Core;

        protected bool isAttackActive;

        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();

            eventHandler = GetComponentInChildren<AnimationEventHandler>();
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void HandleEnter()
        {
            isAttackActive = true;
        }

        protected virtual void HandleExit()
        {
            isAttackActive = false;
        }

        protected virtual void OnEnable()
        {
            weapon.OnEnter += HandleEnter;
            weapon.OnExit += HandleExit;
        }

        protected virtual void OnDisable()
        {
            weapon.OnEnter -= HandleEnter;
            weapon.OnExit -= HandleExit;
        }
    }

    // Generic class that inherits from the above WeaponComponent class.
    // This removes the need for each component to ask the WeaponComponent for it's own data, it will now automatically get it when it is instantiated.
    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2 : AttackData
    {
        protected T1 data;
        protected T2 currentAttackData;

        protected override void HandleEnter()
        {
            base.HandleEnter();

            // Get data for current attack
            currentAttackData = data.AttackData[weapon.CurrentAttackCounter];
        }

        protected override void Awake()
        {
            base.Awake();

            // Data for specific component
            data = weapon.Data.GetData<T1>();
        }
    }
}
