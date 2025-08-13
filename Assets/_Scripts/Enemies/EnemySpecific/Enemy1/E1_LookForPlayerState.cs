using UnityEngine;

public class E1_LookForPlayerState : LookForPlayerState
{
    private Enemy1 enemy;

    public E1_LookForPlayerState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAggroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
