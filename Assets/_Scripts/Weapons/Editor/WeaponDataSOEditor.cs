using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections.Generic;
using System;
using KnightsQuest.Weapons.Components;
using System.Linq;
using UnityEngine.UIElements;

namespace KnightsQuest.Weapons
{
    [CustomEditor(typeof(WeaponDataSO))]
    public class WeaponDataSOEditor : Editor
    {
        // List to hold MovementData and WeaponSpriteData
        private static List<Type> dataComponentTypes = new List<Type>();

        private WeaponDataSO dataSO;

        private bool showForceUpdateButtons;
        private bool showAddComponentButtons;

        private void OnEnable()
        {
            // Get reference to WeaponDataSO that is being inspected
            dataSO = target as WeaponDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set Number of Attacks"))
            {
                foreach (var item in dataSO.ComponentData)
                {
                    item.InitializeAttackData(dataSO.NumberOfAttacks);
                }
            }

            // Adds a foldout for the add component buttons
            showAddComponentButtons = EditorGUILayout.Foldout(showAddComponentButtons, "Add Components");

            if (showAddComponentButtons)
            {
                // Iterate through the list, make a button for each data component type
                foreach (var dataComponentType in dataComponentTypes)
                {
                    if (GUILayout.Button(dataComponentType.Name))
                    {
                        // Create an object that has the type, cast it as ComponentData
                        var comp = Activator.CreateInstance(dataComponentType) as ComponentData;

                        if (comp == null)
                            return;

                        // Give the component the correct number of attacks
                        comp.InitializeAttackData(dataSO.NumberOfAttacks);

                        dataSO.AddData(comp);
                    }
            }
            }
            
            // Adds a foldout for the force update buttons
            showForceUpdateButtons = EditorGUILayout.Foldout(showForceUpdateButtons, "Force Update");

            if (showForceUpdateButtons)
            {
                // Rename components
                if (GUILayout.Button("Force Update Component Names"))
                {
                    foreach (var item in dataSO.ComponentData)
                    {
                        item.SetComponentName();
                    }
                }

                // Rename attacks
                if (GUILayout.Button("Force Update Attack Names"))
                {
                    foreach (var item in dataSO.ComponentData)
                    {
                        item.SetAttackDataNames();
                    }
                }
            }
            
        }

        // Reload the buttons each time we make a change to the code
        [DidReloadScripts]
        private static void OnRecompile()
        {
            // Get all assemblies
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // Get the type of each assembly
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            // Get all types that are a subtype of ComponentData, but not the generic ComponentData class
            var filteredTypes = types.Where(
                type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass
                );
            
            dataComponentTypes = filteredTypes.ToList();
        }
    }
}
