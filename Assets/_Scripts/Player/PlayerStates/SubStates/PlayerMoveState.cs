using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.CheckIfShouldFlip(xInput);

        Movement?.ApplyForce(playerData.movementMaxVelocity, xInput, playerData.movementAccelerationAmount, playerData.movementDecelerationAmount);
        //Movement?.SetVelocityX(playerData.movementVelocity * xInput);


        if (!isExitingState)
        {
            if (xInput == 0 && Movement.CurrentVelocity.x <= 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (yInput == -1)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }
        }
    }
 
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
