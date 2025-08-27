using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    public class ActionHitBoxData : ComponentData<AttackActionHitBox>
    {
        [field: SerializeField] public LayerMask DetectableLayers { get; private set; }

        public ActionHitBoxData()
        {   
            // Set Component Dependency to ActionHitBox
            ComponentDependency = typeof(ActionHitBox);
        }
    }
}