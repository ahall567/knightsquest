using KnightsQuest.Interfaces;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    /// <summary>
    /// Logic component responsible for detecting damageable entities within a hitbox
    /// and applying the damage amount defined in WeaponDataSO.
    /// </summary>
    public class DamageComponent : WeaponComponent<DamageData, AttackDamage>
    {
        private ActionHitBoxComponent hitBox;

        /// <summary>
        /// Triggered by ActionHitBox when it overlaps with physics colliders.
        /// </summary>
        /// <param name="colliders">The list of potential targets detected this frame.</param>
        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(currentAttackData.Amount);
                }
            }
        }

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitBoxComponent>();

            // Subscribe to OnDetectCollider2D Event
            hitBox.OnDetectCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            hitBox.OnDetectCollider2D -= HandleDetectCollider2D;
        }
    }
}