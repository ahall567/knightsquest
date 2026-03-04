using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class KnockBackData : ComponentData<KnockBackStep>
    {
        protected override void SetComponentDependency()
        {
            // Set Component Dependency to KnockBack
            ComponentDependency = typeof(KnockBackComponent);
        }
    }
}
