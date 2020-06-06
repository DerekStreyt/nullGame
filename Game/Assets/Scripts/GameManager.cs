using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform uiParent;
    public HudUI hudPrefab;
    public GameInput input;
    public int score = 0;
    public Unit character;

    public event Action<int> onScoreChanged;
    
    public void StartGame()
    {

    }

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    protected virtual void Awake()
    {
        _instance = this;
    }

    public void CreateDamageHud(int damage)
    {
        HudUI hud = UnityPoolManager.Instance.PopOrCreate(hudPrefab);
        hud.transform.SetParent(uiParent);
        hud.Attach(input.unit.Position, damage.ToString(), Color.red);
    }

    public void CreateWaterHud(float water)
    {
        HudUI hud = UnityPoolManager.Instance.PopOrCreate(hudPrefab);
        hud.transform.SetParent(uiParent);
        hud.Attach(input.unit.Position, water.ToString("F2"), Color.blue);
    }

    private int Score
    {
        get => score;
        set
        {
            score = value;
            onScoreChanged?.Invoke(score);
        }
    }

    public virtual void AddScore(int score)
    {
        this.score += score;
    }
}
