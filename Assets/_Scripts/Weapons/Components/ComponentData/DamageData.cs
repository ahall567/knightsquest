using System;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class DamageData : ComponentData<AttackDamage>
    {
        protected override void SetComponentDependency()
        {
            // Set Component Dependency to Damage
            ComponentDependency = typeof(DamageComponent);
        }
    }
}