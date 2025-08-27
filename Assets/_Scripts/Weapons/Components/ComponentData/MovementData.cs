using KnightsQuest.Weapons.Components;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    public class MovementData : ComponentData<AttackMovement>
    {
        protected override void SetComponentDependency()
        {
            // Set Component Depency to Movement
            ComponentDependency = typeof(Movement);
        }
    }
}
