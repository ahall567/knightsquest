using UnityEngine;

public class PlayerState
{
    protected Core core;

    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animationBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animationBoolName = animationBoolName;
        core = player.Core;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.Anim.SetBool(animationBoolName, true);
        startTime = Time.time;
        Debug.Log(animationBoolName);

        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animationBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
 
}
