using KnightsQuest.Weapons.Components;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    public class WeaponSpriteData : ComponentData<AttackSprites>
    {
        protected override void SetComponentDependency()
        {
            // Set Component Dependency to WeaponSprite
            ComponentDependency = typeof(WeaponSprite);
        }
    }
}
