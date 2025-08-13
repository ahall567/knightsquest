using UnityEngine;

public class E1_MeleeAttackState : MeleeAttackState
{
    private Enemy1 enemy;

    public E1_MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInMinAggroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }
}
