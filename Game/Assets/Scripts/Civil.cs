using UnityEngine;

public class Civil : Character
{
    public static int Count;
    
    protected override void Awake()
    {
        base.Awake();
        Count++;
    }

    protected override void OnDie()
    {
        base.OnDie();
        Count--;
        Debug.Log("Civil was died left " + Count);
        if (Count == 0)
        {
            GameManager.Instance.Lose();
        }
    }
}
