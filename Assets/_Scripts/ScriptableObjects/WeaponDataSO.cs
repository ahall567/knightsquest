using System;
using System.Collections.Generic;
using System.Linq;
using KnightsQuest.Weapons.Components;
using UnityEngine;

namespace KnightsQuest.Weapons
{
    /// <summary>
    /// A data container that defines a weapon's stats and required components.
    /// </summary>
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data")]
    public class WeaponDataSO : ScriptableObject
    {
        [field: Header("Attack Settings")]
        [field: Tooltip("The number of attacks in this weapon's combo chain.")]
        [field: SerializeField] public int NumberOfAttacks { get; private set; }

        /// <summary>
        /// A polymorphic dynamic list of data for various weapon components (Damage, Knockback, etc.)
        /// SerializeReference allows different subclasses of ComponentData to exist in this list.
        /// </summary>
        [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

        /// <summary>
        /// Searches the data list for a specific type of ComponentData.
        /// Used by WeaponComponents to "find" their specific settings.
        /// </summary>
        /// <typeparam name="T">The type of ComponentData to search for (e.g., DamageData).</typeparam>
        /// <returns>The data object if found; otherwise, null.</returns>
        public T GetData<T>()
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Extracts the C# System.Type of every component required by this weapon.
        /// This is used by the WeaponGenerator to know which scripts to AddComponent() to the weapon.
        /// </summary>
        /// <returns></returns>
        public List<Type> GetAllDependencies()
        {
            return ComponentData.Select(component => component.ComponentDependency).ToList();
        }

        /// <summary>
        /// Helper method to programmatically add new data types to the SO while ensuring
        /// there is no more than one of each type.
        /// </summary>
        /// <param name="data"></param>
        public void AddData(ComponentData data)
        {
            // Prevents duplicate data types
            if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
                return;

            ComponentData.Add(data);
        }
    }
}
