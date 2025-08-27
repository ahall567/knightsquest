using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class ComponentData
    {
        [SerializeField, HideInInspector] private string name;

        // Used to specify which type of data a specific component is
        public Type ComponentDependency { get; protected set; }

        public ComponentData()
        {
            // Set Component Name any time a new instance is created
            SetComponentName();
        }

        // Sets the name of the Inspector Element to the Component's name
        public void SetComponentName() => name = GetType().Name;

        public virtual void SetAttackDataNames() {}
        
        public virtual void InitializeAttackData(int numberOfAttacks){}
    }

    // Allows for WeaponComponent to call specifically for AttackData
    [Serializable]
    public class ComponentData<T> : ComponentData where T : AttackData
    {
        [SerializeField] private T[] attackData;
        public T[] AttackData { get => attackData; private set => attackData = value; }

        public override void SetAttackDataNames()
        {
            base.SetAttackDataNames();

            for (int i = 0; i < AttackData.Length; i++)
            {
                AttackData[i].SetAttackName(i + 1);
            }
        }

        public override void InitializeAttackData(int numberOfAttacks)
        {
            base.InitializeAttackData(numberOfAttacks);

            // Get length of current AttackData. If it has not been initialized, set it to 0
            var oldLength = attackData != null ? attackData.Length : 0;

            // If the legnths are the same, no resize is needed
            if (oldLength == numberOfAttacks)
                return;

            // Resize the array to numberOfAttacks
            Array.Resize(ref attackData, numberOfAttacks);

            // If the new Array is bigger, start initializing after the already intialized objects
            // Prevents overwriting of objects
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
