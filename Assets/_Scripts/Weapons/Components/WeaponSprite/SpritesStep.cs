using System;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    [Serializable]
    public class SpritesStep : AttackData
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }
}
