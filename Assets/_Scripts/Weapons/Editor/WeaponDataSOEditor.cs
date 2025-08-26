using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections.Generic;
using System;
using KnightsQuest.Weapons.Components;
using System.Linq;

namespace KnightsQuest.Weapons
{
    [CustomEditor(typeof(WeaponDataSO))]

    public class WeaponDataSOEditor : Editor
    {
        // List to hold MovementData and WeaponSpriteData
        private static List<Type> dataComponentTypes = new List<Type>();

        private WeaponDataSO dataSO;

        private void OnEnable()
        {
            // Get reference to WeaponDataSO that is being inspected
            dataSO = target as WeaponDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Iterate through the list, make a button for each type
            foreach (var dataComponentType in dataComponentTypes)
            {
                if (GUILayout.Button(dataComponentType.Name))
                {
                    // Create an object that has the type, cast it as ComponentData
                    var comp = Activator.CreateInstance(dataComponentType) as ComponentData;

                    if (comp == null)
                        return;

                    dataSO.AddData(comp);
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
