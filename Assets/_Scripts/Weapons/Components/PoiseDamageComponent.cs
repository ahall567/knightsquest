using KnightsQuest.Interfaces;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    public class PoiseDamage : WeaponComponent<PoiseDamageData, AttackPoiseDamage>
    {
        private ActionHitBox hitBox;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                // Check if any of the detected objects are poise damageable
                if (item.TryGetComponent(out IPoiseDamageable poiseDamageable))
                {
                    // Poise damage the poise damageables
                    poiseDamageable.DamagePoise(currentAttackData.Amount);
                }
            }
        }

        protected override void Start()
        {
            base.Start();

            // Get reference to the hitbox
            hitBox = GetComponent<ActionHitBox>();

            // Subscribe to OnDetectCollider2D event with handler
            hitBox.OnDetectCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Unsubscribe from OnDetectCollider2D event
            hitBox.OnDetectCollider2D -= HandleDetectCollider2D;
        }
    }
}
