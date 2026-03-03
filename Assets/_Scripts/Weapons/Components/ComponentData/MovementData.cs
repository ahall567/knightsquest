using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class MovementData : ComponentData<AttackMovement>
    {
        protected override void SetComponentDependency()
        {
            // Set Component Depency to Movement
            ComponentDependency = typeof(Movement);
        }
    }
}
