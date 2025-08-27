using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    public class KnockBack : WeaponComponent<KnockBackData, AttackKnockBack>
    {
        private ActionHitBox hitBox;

        private CoreSystem.Movement movement;

        private void HandleDetectCollider2D(Collider2D[] colliders)
        {
            foreach (var item in colliders)
            {
                if (item.TryGetComponent(out IKnockBackable knockBackable))
                {
                    knockBackable.KnockBack(currentAttackData.Angle, currentAttackData.Strength, movement.FacingDirection);
                }
            }
        }

        protected override void Start()
        {
            base.Start();

            hitBox = GetComponent<ActionHitBox>();

            // Subscribe to OnDetectCollider2D event with handler function
            hitBox.OnDetectCollider2D += HandleDetectCollider2D;

            // Get reference to Core Movement Component
            movement = Core.GetCoreComponent<CoreSystem.Movement>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Unsubscribe from OnDetectCollider2D event
            hitBox.OnDetectCollider2D -= HandleDetectCollider2D;
        }
    }
}
