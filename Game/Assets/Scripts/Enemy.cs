public class Enemy : UnityPoolObject
{
    public int maxHp = 100;
    public int hp = 100;

    public override void AfterCreate()
    {
        hp = maxHp;
    }

    public virtual void ReceiveDamage( int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Push();
        }
    }
}
