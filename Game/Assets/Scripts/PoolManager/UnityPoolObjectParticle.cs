using UnityEngine;

public class UnityPoolObjectParticle : UnityPoolObjectWithTime
{
    protected ParticleSystem[] particles;

    protected override void Awake()
    {
        base.Awake();
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    public override void OnPush()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Stop();
        }
        base.OnPush();
    }

    public override void AfterCreate()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
}
