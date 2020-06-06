public class Enemy : UnitWithHealth
{
    protected override void OnDie()
    {
        Push();
    }
}
