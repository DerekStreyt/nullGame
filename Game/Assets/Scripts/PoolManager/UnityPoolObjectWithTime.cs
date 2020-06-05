using UnityEngine;

public class UnityPoolObjectWithTime : UnityPoolObject
{
    public float time = 60;

    protected float timer = 0;

    protected virtual void Update()
    {
        if (timer > time)
        {
            Push();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public override void Create()
    {
        timer = 0;
        base.Create();
    }
}
