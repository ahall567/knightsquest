using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class MovementData : ComponentData<MovementStep>
    {
        protected override void SetComponentDependency()
        {
            // Set Component Depency to Movement
            ComponentDependency = typeof(MovementComponent);
        }
    }
}
