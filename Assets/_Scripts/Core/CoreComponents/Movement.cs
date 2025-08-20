using UnityEngine;

namespace KnightsQuest.CoreSystem
{
    // TODO: Update this to use forces in preparation for sliding/skidding mechanics.
    public class Movement : CoreComponent
    {
        public Rigidbody2D RB { get; private set; }
        public int FacingDirection { get; private set; }

        public bool CanSetVelocity { get; set; }

        public Vector2 CurrentVelocity { get; private set; }
        private Vector2 workspace;

        protected override void Awake()
        {
            base.Awake();

            // Get reference to RigidBody2D component that the parent of this script (CoreComponent, Core) is attached to
            RB = GetComponentInParent<Rigidbody2D>();

            // Set FacingDirection to right (1) and CanSetVelocity to true
            FacingDirection = 1;
            CanSetVelocity = true;
        }

        public override void LogicUpdate()
        {
            // Get current velocity of the referenced RigidBody
            CurrentVelocity = RB.linearVelocity;
        }

        #region Set Functions

        // Sets the velocity to zero, stopping the body
        public void SetVelocityZero()
        {
            workspace = Vector2.zero;
            SetFinalVelocity();
        }

        // Set the body's velocity, specifying an angle
        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            workspace.Set(angle.x * velocity * direction, angle.y * velocity);
            SetFinalVelocity();
        }

        // Set the body's velocity
        public void SetVelocity(float velocity, Vector2 direction)
        {
            workspace = direction * velocity;
            SetFinalVelocity();
        }

        // Set the only the body's X velocity
        public void SetVelocityX(float velocity)
        {
            workspace.Set(velocity, CurrentVelocity.y);
            SetFinalVelocity();
        }

        // Set only the body's Y velocity
        public void SetVelocityY(float velocity)
        {
            workspace.Set(CurrentVelocity.x, velocity);
            SetFinalVelocity();
        }

        // If CanSetVelocity is true, apply the velocity in workspace to the body's velocity, update CurrentVelocity to match
        private void SetFinalVelocity()
        {
            if (CanSetVelocity)
            {
                RB.linearVelocity = workspace;
                CurrentVelocity = workspace;
            }
        }

        #endregion

        // If player is providing X Input that does not match the current FacingDirection, call the Flip function
        public void CheckIfShouldFlip(int xInput)
        {
            if (xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }

        public void Flip()
        {
            // Invert FacingDirection
            FacingDirection *= -1;
            // Flip referenced RigidBody
            RB.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
}