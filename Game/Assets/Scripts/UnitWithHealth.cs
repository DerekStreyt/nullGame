using System;

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
    public virtual void ReceiveDamage(int damage)
    {
        if (hp > 0)
        {
            onReceiveDamage?.Invoke(hp, damage);
            hp -= damage;
            if (hp <= 0)
            {
                onDie?.Invoke();
                OnDie();
            }
        }
    }

    protected virtual void OnDie()
    {

    }
}
