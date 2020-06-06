using System;
using UnityEngine;

public class UnitWithHealth : UnityPoolObject
{
    public float maxHp = 100;

    public float hp = 100;

    public event Action onDie;
    public event Action<float> onReceiveDamage; // hp before damage

    public bool IsActive => hp > 0;
    public override void AfterCreate()
    {
        hp = maxHp;
    }

    private float HP
    {
        get => hp;
        set
        {
            hp = value;
            onReceiveDamage?.Invoke(hp);
        }
    }
    
    public virtual bool ReceiveDamage(int damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        bool result = false;
        if (HP > 0)
        {
            HP -= damage;
            if (HP <= 0)
            {
                onDie?.Invoke();
                OnDie();
            }
            result = true;
        }
        return result;
    }

    protected virtual void OnDie()
    {

    }
}
