using UnityEngine;

public class PlayerDetectedState : State
{
    protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    private CollisionSenses CollisionSenses { get => collisionSenses ??= core.GetCoreComponent<CollisionSenses>(); }

    private Movement movement;
    private CollisionSenses collisionSenses;

    protected D_PlayerDetectedState stateData;

    protected bool isPlayerInMinAggroRange;
    protected bool isPlayerInMaxAggroRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingLedge;

    public PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
        isDetectingLedge = CollisionSenses.LedgeVertical;
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;
        Movement?.SetVelocityX(0f);

        DoChecks();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(0f);

        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        DoChecks();
    }
}
