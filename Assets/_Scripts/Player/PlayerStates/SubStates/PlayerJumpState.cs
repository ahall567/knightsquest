using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

// Player is in JumpState if they have just pressed the jump button and they are still in the air
public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft; // Controls double jump

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.UseJumpInput();
        Movement?.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;

}
