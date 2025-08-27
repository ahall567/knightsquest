using KnightsQuest.Interfaces;
using UnityEngine;

namespace KnightsQuest.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField] private GameObject damageParticles;

        private Stats stats;
        private ParticleManager particleManager;

        // Particle effects and decrease health
        public void Damage(float amount)
        {
            Debug.Log(core.transform.parent.name + " Damaged!");
            stats.Health.Decrease(amount);
            particleManager.StartParticlesWithRandomRotation(damageParticles);
        }

        protected override void Awake()
        {
            base.Awake();

            // Get Core Component references
            stats = core.GetCoreComponent<Stats>();
            particleManager = core.GetCoreComponent<ParticleManager>();
        }
    }
}