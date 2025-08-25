using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class ComponentData
    {

    }

    // Generic version of ComponentData class.
    // Allows for WeaponComponent to call specifically for AttackData
    [Serializable]
    public class ComponentData<T> : ComponentData where T : AttackData
    {
        // Array of AttackData
        [field: SerializeField] public T[] AttackData { get; private set; }
    }
}
