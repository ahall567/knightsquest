using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class KnockBackStep : AttackData
    {
        [field: SerializeField] public Vector2 Angle { get; private set; }
        [field: SerializeField] public float Strength { get; private set; }
    }
}
