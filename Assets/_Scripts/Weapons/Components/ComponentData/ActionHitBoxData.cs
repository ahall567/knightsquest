using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class ActionHitBoxData : ComponentData<AttackActionHitBox>
    {
        [field: SerializeField] public LayerMask DetectableLayers { get; private set; }

        protected override void SetComponentDependency()
        {
            // Set Component Dependency to ActionHitBox
            ComponentDependency = typeof(ActionHitBox);
        }
    }
}