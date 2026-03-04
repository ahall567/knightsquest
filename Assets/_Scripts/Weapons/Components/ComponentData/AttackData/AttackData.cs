using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    /// <summary>
    /// The base class for information specific to a singel attack in a combo.
    /// Subclasses (like AttackDamage) add specific variables.
    /// </summary>
    public class AttackData
    {
        [SerializeField, HideInInspector] private string name;

        /// <summary>
        /// Updates the internal name for better readability in the Unity Inspector.
        /// Usually called by ComponentData during initialization or resizing.
        /// </summary>
        /// <param name="i">The index of the attack (1-based for player readability).</param>
        public void SetAttackName(int i) => name = $"Attack {i}";
    }
}