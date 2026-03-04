using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class HitBoxStep : AttackData
    {
        // Debug is used to toggle drawing of gizmos in Unity
        public bool Debug;
        [field: SerializeField] public Rect HitBox { get; private set; }
    }
}