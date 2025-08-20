using System.Collections.Generic;
using System.Linq;
using KnightsQuest.Weapons.Components.ComponentData;
using UnityEngine;

namespace KnightsQuest.Weapons
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data")]
    public class WeaponDataSO : ScriptableObject
    {
        [field: SerializeField] public int NumberOfAttacks { get; private set; }

        [field: SerializeReference] public List<ComponentData> componentData { get; private set; }

        // WeaponComponents use this function to ask componentData for its specific data.
        public T GetData<T>()
        {
            return componentData.OfType<T>().FirstOrDefault();
        }

        // Adds "Add Sprite Data" option to Context Menu of Data Asset
        [ContextMenu("Add Sprite Data")]
        private void AddSpriteData() => componentData.Add(new WeaponSpriteData());
    }
}
