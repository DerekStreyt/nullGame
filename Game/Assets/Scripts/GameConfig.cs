using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public static GameConfig Instance { get; protected set; }

    public float addWater = 20;
    public float addWaterTime = 1;
    public int damageByZone = 10;
    public float damageTime = 1;
    public float hudTime = 2;
    public float hudSpeed = 1;
    public int defaultScore = 1;
    public float fireIncreaseTime = 1f;
    public float fireSpreadingChance = 0.5f;

    protected virtual void Awake()
    {
        Instance = this;
    }
}
