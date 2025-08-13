using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }

    private bool isHolding;

    private float lastDashTime;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.InputHandler.UseDashInput();

        isHolding = true;
        dashDirection = Vector2.right * Movement.FacingDirection;
    }

    public override void Exit()
    {
        base.Exit();

        if(Movement?.CurrentVelocity.y > 0)
        {
            Movement?.SetVelocityY(Movement.CurrentVelocity.y * playerData.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            player.Anim.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));

            if (isHolding)
            {
                dashDirectionInput = player.InputHandler.RawDashDirectionInput;

                if(dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }
                isHolding = false;
            }
            else
            {
                Movement?.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                player.RB.linearDamping = playerData.drag;
                Movement?.SetVelocity(playerData.dashVelocity, dashDirection);

                if(Time.time >= startTime + playerData.dashTime)
                {
                    player.RB.linearDamping = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }

    public void ResetCanDash() => CanDash = true;
}
