using KnightsQuest.CoreSystem;
using KnightsQuest.Weapons.Components.ComponentData;
using UnityEngine;

namespace KnightsQuest.Weapons.Components
{
    public class Movement : WeaponComponent
    {
        private CoreSystem.Movement coreMovement;
        private CoreSystem.Movement CoreMovement { get => coreMovement ??= Core.GetCoreComponent<CoreSystem.Movement>(); }

        private MovementData data;

        private void HandleStartMovement()
        {
            var currentAttackData = data.AttackData[weapon.CurrentAttackCounter];

            CoreMovement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, CoreMovement.FacingDirection);
        }

        private void HandleStopMovement()
        {
            CoreMovement.SetVelocityZero();
        }

        protected override void Awake()
        {
            base.Awake();

            data = weapon.Data.GetData<MovementData>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            eventHandler.OnStartMovement += HandleStartMovement;
            eventHandler.OnStopMovement += HandleStopMovement;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            eventHandler.OnStartMovement -= HandleStartMovement;
            eventHandler.OnStopMovement -= HandleStopMovement;
        }
    }
}
