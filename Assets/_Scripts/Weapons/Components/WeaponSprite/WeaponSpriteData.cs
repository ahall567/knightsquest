using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class WeaponSpriteData : ComponentData<SpritesStep>
    {
        protected override void SetComponentDependency()
        {
            // Set Component Dependency to WeaponSprite
            ComponentDependency = typeof(WeaponSpriteComponent);
        }
    }
}
