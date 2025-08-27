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

        protected override void Awake()
        {
            base.Awake();

            hitBox = GetComponent<ActionHitBox>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            hitBox.OnDetectCollider2D += HandleDetectCollider2D;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            hitBox.OnDetectCollider2D -= HandleDetectCollider2D;
        }
    }
}