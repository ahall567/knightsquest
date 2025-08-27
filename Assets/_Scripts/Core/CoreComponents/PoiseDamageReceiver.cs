using KnightsQuest.Interfaces;
using UnityEngine;

namespace KnightsQuest.CoreSystem
{
    public class PoiseDamageReceiver : CoreComponent, IPoiseDamageable
    {
        private Stats stats;

        public void DamagePoise(float amount)
        {
            stats.Poise.Decrease(amount);
        }

        protected override void Awake()
        {
            base.Awake();

            // Get Core Component reference
            stats = core.GetCoreComponent<Stats>();
        }
    }
}
