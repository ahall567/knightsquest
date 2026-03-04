using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class HitBoxData : ComponentData<HitBoxStep>
    {
        [field: SerializeField] public LayerMask DetectableLayers { get; private set; }

        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(HitBoxComponent);
        }
    }
}