using UnityEngine;

public class PlayerStoppingState : PlayerGroundedState
{
    public PlayerStoppingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Stopping State");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.ApplyForce(playerData.movementMaxVelocity, 0, playerData.movementAccelerationAmount, playerData.movementDecelerationAmount);

        if (Mathf.Abs(Movement.CurrentVelocity.x) <= 0.1f)
        {
            if (xInput == 0)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
        }

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
