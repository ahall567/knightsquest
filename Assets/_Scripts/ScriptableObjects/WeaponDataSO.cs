using System;
using System.Collections.Generic;
using System.Linq;
using KnightsQuest.Weapons.Components;
using UnityEngine;

namespace KnightsQuest.Weapons
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data")]
    public class WeaponDataSO : ScriptableObject
    {
        [field: SerializeField] public int NumberOfAttacks { get; private set; }

        [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

        // WeaponComponents use this function to ask componentData for its specific data.
        public T GetData<T>()
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }

        public List<Type> GetAllDependencies()
        {
            // Get a list of dependencies of all components
            return ComponentData.Select(component => component.ComponentDependency).ToList();
        }

        // Add a new ComponentData
        public void AddData(ComponentData data)
        {
            // Make sure a ComponentData of this type does not already exist
            if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
                return;

            ComponentData.Add(data);
        }
    }
}
