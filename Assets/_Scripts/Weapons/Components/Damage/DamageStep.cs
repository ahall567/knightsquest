using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class DamageStep : AttackData
    {
        [field: SerializeField] public float Amount { get; private set; }
    }
}
