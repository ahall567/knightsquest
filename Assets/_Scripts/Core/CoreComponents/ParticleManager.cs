using UnityEngine;

public class ParticleManager : CoreComponent
{
    private Transform particleContainer;

    protected override void Awake()
    {
        base.Awake();

        particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
    }

    // Spawns particles
    public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation)
    {
        return Instantiate(particlePrefab, position, rotation, particleContainer);
    }

    // Spawns particles at the position of the entity
    public GameObject StartParticles(GameObject particlePrefab)
    {
        return StartParticles(particlePrefab, transform.position, Quaternion.identity);
    }

    // Spawns particles at the position of the entity with random rotation
    public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab)
    {
        var randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        return StartParticles(particlePrefab, transform.position, randomRotation);
    }
}
