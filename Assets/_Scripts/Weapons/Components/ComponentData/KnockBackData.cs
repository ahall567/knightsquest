using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    public class KnockBackData : ComponentData<AttackKnockBack>
    {
        protected override void SetComponentDependency()
        {
            // Set Component Dependency to KnockBack
            ComponentDependency = typeof(KnockBack);
        }
    }
}
