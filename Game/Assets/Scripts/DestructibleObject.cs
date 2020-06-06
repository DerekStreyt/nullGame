public class DestructibleObject : UnitWithHealth
{
    protected override void OnDie()
    {
        GameManager.Instance.AddScore(GameConfig.Instance.defaultScore);
        Push();
    } 
}
