using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    /// <summary>
    /// The base class for all weapon component data.
    /// Handles identification and links the data to its funcitonal logic script (the depency).
    /// </summary>
    [Serializable]
    public abstract class ComponentData
    {
        [SerializeField, HideInInspector] private string name;

        /// <summary>
        /// The Type of the WeaponComponent script that this data belongs to.
        /// Used by the Generator to know which script to attach to the Weapon GameObject.
        /// </summary>
        public Type ComponentDependency { get; protected set; }

        public ComponentData()
        {
            SetComponentName();
            SetComponentDependency();
        }

        /// <summary> Sets the internal name field so the Unity Inspector list is readable. </summary>
        public void SetComponentName() => name = GetType().Name;

        /// <summary> Must be implemented to define which script handles this data's logic. </summary>
        protected abstract void SetComponentDependency();

        public virtual void SetAttackDataNames() { }

        /// <summary> Called to ensure the data arrays match the number of attacks in a combo. </summary>
        public virtual void InitializeAttackData(int numberOfAttacks) { }
    }

    /// <summary>
    /// A generic wrapper for data that varies per attack (e.g., Damage, Knockback, Poise).
    /// Automatically manages an array of AttackData objects based on the weapon's combo length.
    /// </summary>
    /// <typeparam name="T"> A class inheriting from AttackData. </typeparam>
    [Serializable]
    public abstract class ComponentData<T> : ComponentData where T : AttackData
    {
        [SerializeField] private T[] attackData;
        public T[] AttackData { get => attackData; private set => attackData = value; }

        /// <summary> Updates the display names of each attack element in the Inspector. </summary>
        public override void SetAttackDataNames()
        {
            base.SetAttackDataNames();

            for (int i = 0; i < AttackData.Length; i++)
            {
                AttackData[i].SetAttackName(i + 1);
            }
        }

        /// <summary>
        /// Resizes the attack data array to match the weapon's combo count.
        /// Uses reflection to instantiate new data objects without losing existing ones.
        /// </summary>
        public override void InitializeAttackData(int numberOfAttacks)
        {
            base.InitializeAttackData(numberOfAttacks);

            var oldLength = attackData != null ? attackData.Length : 0;

            if (oldLength == numberOfAttacks)
                return;

            // Resize the array while preserving existing data
            Array.Resize(ref attackData, numberOfAttacks);

            // If the array grew, fill the new slots with fresh instances of the data type
            if (oldLength < numberOfAttacks)
            {
                for (var i = oldLength; i < attackData.Length; i++)
                {
                    var newObj = Activator.CreateInstance(typeof(T)) as T;
                    attackData[i] = newObj;
                }
            }

            SetAttackDataNames();
        }
    }
}
