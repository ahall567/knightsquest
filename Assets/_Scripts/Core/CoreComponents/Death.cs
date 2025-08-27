using UnityEngine;

namespace KnightsQuest.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;

        private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent<ParticleManager>();
        private ParticleManager particleManager;

        private Stats Stats => stats ? stats : core.GetCoreComponent<Stats>();
        private Stats stats;

        public void Die()
        {
            foreach (var particle in deathParticles)
            {
                ParticleManager.StartParticles(particle);
            }

            core.transform.parent.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Stats.Health.OnCurrentValueZero += Die;
        }

        private void OnDisable()
        {
            Stats.Health.OnCurrentValueZero -= Die;
        }
    }
}