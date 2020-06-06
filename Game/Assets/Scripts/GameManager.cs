using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform uiParent;
    public HudUI hudPrefab;
    public GameInput input;

    public void StartGame()
    {
        
    }
    
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }
    }

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
        hud.Attach(input.unit.Position, water.ToString("F2"), Color.green);
    }
}
