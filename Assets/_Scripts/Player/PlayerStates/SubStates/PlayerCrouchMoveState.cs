using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetColliderHeight(playerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();

        player.SetColliderHeight(playerData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            Movement?.SetVelocityX(playerData.crouchMovementVelocity * Movement.FacingDirection);
            Movement?.CheckIfShouldFlip(xInput);

            if (xInput == 0)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else if (yInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }
    }
}
