using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    public class Damage : WeaponComponent<DamageData, AttackDamage>
    {
        private ActionHitBox hitBox;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                // Check if any of the detected objects are damageable
                if (item.TryGetComponent(out IDamageable damageable))
                {
                    // Damage the damageable objects
                    damageable.Damage(currentAttackData.Amount);
                }
            }
        }

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitBox>();

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