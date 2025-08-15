using KnightsQuest.Weapons;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;

    public PlayerAttackState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animationBoolName,
        Weapon weapon
        ) : base(player, stateMachine, playerData, animationBoolName)
    {
        this.weapon = weapon;
    }

    public override void Enter()
    {
        base.Enter();

        weapon.Enter();
    }
}