using System;
using UnityEngine;

public class UnitWithHealth : UnityPoolObject
{
    public int maxHp = 100;

    public int hp = 100;

    public event Action onDie;
    public event Action<int, int> onReceiveDamage; // hp before damage

    public float Hp01 => (float)hp / maxHp;
    public bool IsActive => hp > 0;
    public override void AfterCreate()
    {
        hp = maxHp;
    }
    public virtual bool ReceiveDamage(int damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        bool result = false;
        if (hp > 0)
        {
            onReceiveDamage?.Invoke(hp, damage);
            hp -= damage;
            if (hp <= 0)
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
