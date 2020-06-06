using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public int score = 10;

    protected bool isUsed = false;
    public virtual void Apply()
    {
        if (!isUsed)
        {
            GameManager.Instance.AddScore(score);
            isUsed = true;
        }
    }
}
