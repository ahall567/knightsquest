using UnityEngine;

public class AttackState : State
{
    private Movement Movement {  get => movement ??= core.GetCoreComponent<Movement>(); }
    private Movement movement;
    
    protected Transform attackPosition;

    protected bool isAnimationFinished;
    protected bool isPlayerInMinAggroRange;

    public AttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(etity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public override void Enter()
    {
        base.Enter();

        entity.atsm.attackState = this;
        isAnimationFinished = false;
        Movement?.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(0f);
    }


    public virtual void TriggerAttack()
    {

    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }

}
