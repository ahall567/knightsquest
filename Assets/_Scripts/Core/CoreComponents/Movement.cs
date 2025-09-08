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
            workspace = Vector3.zero;
            SetFinalVelocity();
        }

        // TODO: Fix this functionality. Right now, the character still slowly moves after changing to idle state.
        // It also works different when going right vs. Left. Right takes a second to decelerate before stopping, left stops immediatley

        // Testing out the use of Force for movement
        public void ApplyForce(float velocity, int xInput, float acceleration, float deceleration)
        {
            Debug.Log(CurrentVelocity.x);
            Debug.Log($"xInput: {xInput}");
            // Calculate speed character wants to get to
            float targetSpeed = xInput * velocity;

            // Smooths changes to direction and speed I guess (idk it was in the tutorial, need to mess around with it)
            if (targetSpeed != 0)
            {
                targetSpeed = Mathf.Lerp(CurrentVelocity.x, targetSpeed, 0.5f);
            }

            // Calculate acceleration rate
            Debug.Log($"targetSpeed Abs: {Mathf.Abs(targetSpeed)}");
            float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

            // Don't apply acceleration if player is facing desired direction but at greater speed than max
            if (Mathf.Abs(CurrentVelocity.x) > Mathf.Abs(targetSpeed) && FacingDirection == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f)
            {
                accelerationRate = 0;
            }
            Debug.Log($"accelerationRate: {accelerationRate}");

            // Calculate difference between curernt velocity and desired velocity
            float speedDiff = targetSpeed - CurrentVelocity.x;

            // Calculate force to apply
            float force = speedDiff * accelerationRate;

            // Apply force, update CurrentVelocity to match
            RB.AddForce(force * Vector2.right);
            CurrentVelocity = RB.linearVelocity;

        }

        // public void ApplyFriction()
        // {
        //     float amount = Mathf.Min(Mathf.Abs(CurrentVelocity.x), Mathf.Abs(0.2f));
        //     amount *= Mathf.Sign(CurrentVelocity.x);

        //     RB.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        //     CurrentVelocity = RB.linearVelocity;
        // }

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