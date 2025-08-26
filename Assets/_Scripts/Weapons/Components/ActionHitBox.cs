using System;
using KnightsQuest.CoreSystem;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        private event Action<Collider2D[]> OnDetectedCollider2D;

        private CoreComp<CoreSystem.Movement> movement;

        private Vector2 offset;

        private Collider2D[] detected;

        private void HandleAttackAction()
        {
            // offset is used to position the HitBox
            offset.Set(
                transform.position.x + (currentAttackData.HitBox.x * movement.Comp.FacingDirection),
                transform.position.y + currentAttackData.HitBox.y
            );

            // Get a list of all detected objects
            detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, data.DetectableLayers);

            // Break out if nothing is detected
            if (detected.Length == 0)
                return;

            // Raise event with detected objects
            OnDetectedCollider2D?.Invoke(detected);

            // Print each item name to the console
            foreach (var item in detected)
            {
                Debug.Log(item.name);
            }

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

        // Draw the hitbox for the current attack sprite
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