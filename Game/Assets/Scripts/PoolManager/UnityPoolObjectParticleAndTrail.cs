using System.Collections;
using UnityEngine;

public class UnityPoolObjectParticleAndTrail : UnityPoolObjectParticle 
{
    protected TrailRenderer[] trails;

    protected override void Awake()
    {
        base.Awake();
        trails = GetComponentsInChildren<TrailRenderer>();
    }

    public override void OnPush()
    {
        foreach (TrailRenderer trail in trails)
        {
            trail.enabled = false;
        }
        base.OnPush();
    }

    public override void AfterCreate()
    {
        base.AfterCreate();
        StartCoroutine(AfterCreateC());
    }

    protected IEnumerator AfterCreateC()
    {
        yield return null;
        foreach (TrailRenderer trail in trails)
        {
            trail.enabled = true;
        }
    }
}
