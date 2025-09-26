using System.Runtime.CompilerServices;
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

        weapon.OnExit += ExitHandler;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Get attack inputs
        bool attackInputs = player.InputHandler.AttackInputs[(int)CombatInputs.primary];

        // If combat input is received while weapon is in recovery state, attack again
        if (attackInputs && weapon.StateMachine.CurrentState == weapon.RecoveryState)
        {
            weapon.Enter();
        }
    }

    public override void Enter()
    {
        base.Enter();

        weapon.Enter();
    }

    private void ExitHandler()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
}