using KnightsQuest.Weapons.Components.ComponentData.AttackData;
using UnityEngine;

namespace KnightsQuest.Weapons.Components.ComponentData
{
    public class WeaponSpriteData : ComponentData
    {
        [field: SerializeField] public AttackSprites[] AttackData { get; set; }
    }
}
