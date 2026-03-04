using System;
using System.Linq;
using System.Collections.Generic;
using KnightsQuest.Weapons.Components;
using UnityEngine;
using UnityEditor.EditorTools;

namespace KnightsQuest.Weapons
{
    /// <summary>
    /// Dynamically constructs a weapon by adding or removing WeaponComponents
    /// at runtime based on requirements defined in WeaponDataSO.
    /// </summary>
    public class WeaponGenerator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Weapon weapon;
        [Tooltip("Data container that defines which components this weapon needs.")]
        [SerializeField] private WeaponDataSO data;

        // Internal tracking lists to manage component lifecycle
        private List<WeaponComponent> componentsAlreadyOnWeapon = new List<WeaponComponent>();
        private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
        private List<Type> componentDepencies = new List<Type>();

        private void Start()
        {
            // TODO: Come back to this after implementing unarmed attacking. May need to remove depending on how unarmed is implemented.
            // Automatically build the weapon when the game starts
            GenerateWeapon(data);
        }

        /// <summary>
        /// Allows you to trigger a weapon rebuild directly fromt he Inspector.
        /// </summary>
        [ContextMenu("Test Generate")]
        private void TestGeneration()
        {
            GenerateWeapon(data);
        }

        /// <summary>
        /// Core assembly logic. Syncs the GameObject's with the provided WeaponDataSO.
        /// </summary>
        /// <param name="data"> WeaponDataSO containing weapon stats and component types. </param>
        public void GenerateWeapon(WeaponDataSO data)
        {
            weapon.SetData(data);

            // Reset tracking state
            componentsAlreadyOnWeapon.Clear();
            componentsAddedToWeapon.Clear();
            componentDepencies.Clear();

            // Map out what we currently have vs. what we need
            componentsAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();
            componentDepencies = data.GetAllDependencies();

            foreach (var dependency in componentDepencies)
            {
                // Skip if we've already processed this dependency type in this loop
                if (componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
                    continue;

                // Check if the component already exists on the GameObject
                var weaponComponent = componentsAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

                // Add missing components
                if (weaponComponent == null)
                {
                    weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;

                }

                // Re-initialize the Weapon Component
                weaponComponent.Init();

                // Mark as 'active' so it isn't deleted in the cleanup phase
                componentsAddedToWeapon.Add(weaponComponent);
            }

            // Cleanup: Remove any WeaponComponents that are no longer required by the WeaponDataSO
            var componentsToRemove = componentsAlreadyOnWeapon.Except(componentsAddedToWeapon);

            // Remove the components
            foreach (var weaponComponent in componentsToRemove)
            {
                Destroy(weaponComponent);
            }
        }
    }
}
