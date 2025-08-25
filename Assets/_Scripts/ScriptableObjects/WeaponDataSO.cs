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

        // Adds "Add Sprite Data" option to Context Menu of Data Asset
        [ContextMenu("Add Sprite Data")]
        private void AddSpriteData() => ComponentData.Add(new WeaponSpriteData());

        [ContextMenu("Add Movement Data")]
        private void AddMovementData() => ComponentData.Add(new MovementData());
    }
}
