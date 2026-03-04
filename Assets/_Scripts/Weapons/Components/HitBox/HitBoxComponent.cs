using System;
using KnightsQuest.CoreSystem;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    /// <summary>
    /// Handles the physical detection of targets in the game world.
    /// Calculates a 2D overlap at a specific frame of animation and notifies listeners.
    /// </summary>
    public class HitBoxComponent : WeaponComponent<HitBoxData, HitBoxStep>
    {
        /// <summary>Broadcastst a list of all colliders caught in the hitbox this frame.</summary>
        public event Action<Collider2D[]> OnDetectCollider2D;

        private CoreComp<CoreSystem.Movement> movement;
        private Vector2 offset;
        private Collider2D[] detected;

        /// <summary>
        /// The main detection logic. Triggered by the Animation Event 'OnAttackAction'.
        /// </summary>
        private void HandleAttackAction()
        {
            // 1. Calculate the spatial offset.
            // We multiply the x center by FacingDirection so the hitbox flips with the player.
            offset.Set(
                transform.position.x + (currentAttackData.HitBox.center.x * movement.Comp.FacingDirection),
                transform.position.y + currentAttackData.HitBox.center.y
            );

            // 2. Perform the physics check
            // DetectableLayers is defined in the WeaponDataSO to ensure we don't 'hit' the floor or player.
            detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, data.DetectableLayers);

            if (detected.Length == 0)
                return;

            // Notify other components
            OnDetectCollider2D?.Invoke(detected);

            // TODO: Add handler for hitbox collision events
        }

        protected override void Start()
        {
            base.Start();

            // Subscribe to OnAttackAction event
            eventHandler.OnAttackAction += HandleAttackAction;

            // Initialize a reference to the Movement system to track which way the player is facing.
            movement = new CoreComp<CoreSystem.Movement>(Core);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (eventHandler != null)
            {
                // Unsubscribe from OnAttackAction event
                eventHandler.OnAttackAction -= HandleAttackAction;
            }
        }

        /// <summary>
        /// Editor-only function. Draws the hitbox in the Scene view when the object is selected.
        /// Essential for aligning hitboxes with animation sprites.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            if (data == null)
                return;

            foreach (var item in data.AttackData)
            {
                // Draw the Gizmo if the current attack's Debug checkbox is selected in the inspector
                if (!item.Debug)
                    continue;

                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
            }
        }
    }
}