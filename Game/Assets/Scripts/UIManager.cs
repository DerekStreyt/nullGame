﻿using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button objectivesButton;
    public GameObject objectivesPanel;
    public Button closeButton;

    public Slider hpBar;
    public Slider waterBar;
    
    // Start is called before the first frame update
    void Start()
    {
        objectivesButton.onClick.AddListener(OnObjectivesButtonClick);
        closeButton.onClick.AddListener(OnObjectivesButtonClick);

        waterBar.maxValue = GameManager.Instance.input.unit.maxWater;
        GameManager.Instance.input.unit.onWaterChange += OnWaterChanged;
        OnWaterChanged(waterBar.maxValue);
        
        hpBar.maxValue = GameManager.Instance.input.unit.maxHp;
        GameManager.Instance.input.unit.onReceiveDamage += OnHealthChanged;
        OnHealthChanged(hpBar.maxValue);
    }

    private void OnHealthChanged(float hp)
    {
        hpBar.value = hp;
    }
    
    private void OnWaterChanged(float water)
    {
        waterBar.value = water;
    }
    
    private void OnObjectivesButtonClick()
    {
        objectivesPanel.SetActive(!objectivesPanel.activeSelf);
    }
}
