using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class PoiseDamageData : ComponentData<AttackPoiseDamage>
    {
        protected override void SetComponentDependency()
        {
            // Set dependency to PoiseDamage
            ComponentDependency = typeof(PoiseDamageComponent);
        }
    }
}
