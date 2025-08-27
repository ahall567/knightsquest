using KnightsQuest.CoreSystem.StatsSystem;
using UnityEngine;

namespace KnightsQuest.CoreSystem
{
    public class Stats : CoreComponent
    {
        [field: SerializeField] public Stat Health { get; private set; }
        [field: SerializeField] public Stat Poise { get; private set; }

        [SerializeField] private float poiseRecoveryRate;

        protected override void Awake()
        {
            base.Awake();

            Health.Init();
            Poise.Init();
        }

        private void Update()
        {
            // If Poise is at max value, do nothing
            if (Poise.CurrentValue == Poise.MaxValue)
                return;

            // Increase poise
            Poise.Increase(poiseRecoveryRate * Time.deltaTime);

        }

    }
}