using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    public class PoiseDamageData : ComponentData<AttackPoiseDamage>
    {
        protected override void SetComponentDependency()
        {
            // Set dependency to PoiseDamage
            ComponentDependency = typeof(PoiseDamage);
        }
    }
}
