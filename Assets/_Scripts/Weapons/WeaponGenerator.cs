using System;
using System.Linq;
using System.Collections.Generic;
using KnightsQuest.Weapons.Components;
using UnityEngine;
using Mono.Cecil;

namespace KnightsQuest.Weapons
{
    // Responsible for all logic for adding and removing Weapon Components from our Weapon Game Object
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private WeaponDataSO data;

        private List<WeaponComponent> componentsAlreadyOnWeapon = new List<WeaponComponent>();
        private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
        private List<Type> componentDepencies = new List<Type>();

        private void Start()
        {
            GenerateWeapon(data);
        }

        [ContextMenu("Test Generate")]
        private void TestGeneration()
        {
            GenerateWeapon(data);
        }

        public void GenerateWeapon(WeaponDataSO data)
        {
            weapon.SetData(data);

            // Empty all of the lists
            componentsAlreadyOnWeapon.Clear();
            componentsAddedToWeapon.Clear();
            componentDepencies.Clear();

            // Get the components that are on the current weapon
            componentsAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();

            // Get all dependencies
            componentDepencies = data.GetAllDependencies();

            foreach (var dependency in componentDepencies)
            {
                // If the component has already been added based on the dependencies, skip it
                if (componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
                    continue;

                // Check if weapon already has component on it
                var weaponComponent = componentsAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

                // If component was not found on weapon, add it
                if (weaponComponent == null)
                {
                    weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;

                }

                // Re-initialize the Weapon Component
                weaponComponent.Init();

                // Track the added component
                componentsAddedToWeapon.Add(weaponComponent);
            }

            // Get a list of components all components on the weapon that weren't added by our dependencies
            var componentsToRemove = componentsAlreadyOnWeapon.Except(componentsAddedToWeapon);

            // Remove the components
            foreach (var weaponComponent in componentsToRemove)
            {
                Destroy(weaponComponent);
            }
        }
    }
}
