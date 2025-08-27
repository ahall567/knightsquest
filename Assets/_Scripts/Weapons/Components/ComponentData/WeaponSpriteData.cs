using KnightsQuest.Weapons.Components;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    public class WeaponSpriteData : ComponentData<AttackSprites>
    {
        public WeaponSpriteData()
        {
            // Set Component Dependency to WeaponSprite
            ComponentDependency = typeof(WeaponSprite);
        }
    }
}
