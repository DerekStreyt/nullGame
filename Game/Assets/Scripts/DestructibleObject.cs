public class DestructibleObject : UnitWithHealth
{
    protected override void OnDie()
    {
        Push();
    } 
}
