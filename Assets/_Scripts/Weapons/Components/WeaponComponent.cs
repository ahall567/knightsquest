using KnightsQuest.CoreSystem;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    /// <summary>
    /// Base class for all functional parts of a weapon.
    /// Manages internal state and subscribes to the Weapon's lifecycle events.
    /// </summary>
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;

        // TODO: Fix this when finishing weapon data
        // protected AnimationEventHandler EventHandler => weapon.EventHandler;
        protected AnimationEventHandler eventHandler;

        /// <summary> Access point to the core entity systems. </summary>
        protected Core Core => weapon.Core;

        protected bool isAttackActive;

        /// <summary>
        /// Called by the WeaponGenerator to set up the component after it's added to the GameObject.
        /// </summary>
        public virtual void Init() { }

        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();
            eventHandler = GetComponentInChildren<AnimationEventHandler>();
        }

        protected virtual void Start()
        {
            // Connect to the weapon's lifecycle
            weapon.OnEnter += HandleEnter;
            weapon.OnExit += HandleExit;
        }

        /// <summary> Called when the weapon starts an attack. </summary>
        protected virtual void HandleEnter()
        {
            isAttackActive = true;
        }

        /// <summary> Called when the weapon attack sequence ends. </summary>
        protected virtual void HandleExit()
        {
            isAttackActive = false;
        }

        protected virtual void OnDestroy()
        {
            // Unsubscribe to prevent memeory leaks or calling logic on destroyed objects.
            if (weapon != null)
            {
                weapon.OnEnter -= HandleEnter;
                weapon.OnExit -= HandleExit;
            }
        }
    }

    /// <summary>
    /// An advanced version of WeaponComponent that automatically links itself to its DataSO.
    /// </summary>
    /// <typeparam name="T1">The type of ComponentData this component uses.</typeparam>
    /// <typeparam name="T2">The type of AttackData (per-swing stats) this component uses.</typeparam>
    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2 : AttackData
    {
        /// <summary>The global data for this component.</summary>
        protected T1 data;

        /// <summary>The specific data for the CURRENT swing in the combo.</summary>
        protected T2 currentAttackData;

        /// <summary>
        /// Automatically updates 'currentAttackData' based on the weapon's CurrentAttackCounter.
        /// </summary>
        protected override void HandleEnter()
        {
            base.HandleEnter();

            // Get data for current step of the combo
            currentAttackData = data.AttackData[weapon.CurrentAttackCounter];
        }

        /// <summary>
        /// Fetches the required data block from the Weapon's ScriptableObject.
        /// </summary>
        public override void Init()
        {
            base.Init();

            // Data for specific component (e.g., find DamageData within WeaponDataSO).
            data = weapon.Data.GetData<T1>();
        }
    }
}
