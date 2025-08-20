using System;
using KnightsQuest.Weapons.Components.ComponentData.AttackData;
using UnityEngine;

namespace KnightsQuest.Weapons.Components.ComponentData
{
    public class MovementData : ComponentData
    {
        [field: SerializeField] public AttackMovement[] AttackData { get; private set; }
    }
}
