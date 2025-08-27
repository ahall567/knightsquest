using System;
using KnightsQuest.CoreSystem;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        public event Action<Collider2D[]> OnDetectCollider2D;

        private CoreComp<CoreSystem.Movement> movement;

        private Vector2 offset;

        private Collider2D[] detected;

        private void HandleAttackAction()
        {
            // offset is used to position the HitBox
            offset.Set(
                transform.position.x + (currentAttackData.HitBox.center.x * movement.Comp.FacingDirection),
                transform.position.y + currentAttackData.HitBox.center.y
            );

            // Get a list of all detected objects
            detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, data.DetectableLayers);

            // Break out if nothing is detected
            if (detected.Length == 0)
                return;

            // Raise event with detected objects
            OnDetectCollider2D?.Invoke(detected);

            // TODO: Add handler for hitbox collision events
        }

        protected override void Start()
        {
            base.Start();

            // Give ActionHitBox access to the Movement Core
            movement = new CoreComp<CoreSystem.Movement>(Core);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            // Subscribe to OnAttackAction event
            eventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            // Unsubscribe from OnAttackAction event
            eventHandler.OnAttackAction -= HandleAttackAction;
        }

        // Draw the hitbox for the selected attack animation
        private void OnDrawGizmosSelected()
        {
            if (data == null)
                return;

            foreach (var item in data.AttackData)
            {
                // Draw the Gizmo if the current attack's Debug checkbox is selected in the inspector
                if (!item.Debug)
                    continue;

                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
                
            }
        }
    }
}