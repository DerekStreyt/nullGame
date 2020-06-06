using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button objectivesButton;
    public GameObject objectivesPanel;
    public Button closeButton;

    public Slider hpBar;
    public Slider waterBar;

    public Text scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        objectivesButton.onClick.AddListener(OnObjectivesButtonClick);
        closeButton.onClick.AddListener(OnObjectivesButtonClick);

        waterBar.maxValue = GameManager.Instance.character.maxWater;
        GameManager.Instance.character.onWaterChange += OnWaterChanged;
        OnWaterChanged(waterBar.maxValue);
        
        hpBar.maxValue = GameManager.Instance.character.maxHp;
        GameManager.Instance.character.onReceiveDamage += OnHealthChanged;
        OnHealthChanged(hpBar.maxValue);

        GameManager.Instance.onScoreChanged += OnScoreChanged;
        OnScoreChanged(0);
    }

    private void OnHealthChanged(float hp)
    {
        hpBar.value = hp;
    }
    
    private void OnWaterChanged(float water)
    {
        waterBar.value = water;
    }

    private void OnScoreChanged(int score)
    {
        scoreText.text = score.ToString();
    }
    
    private void OnObjectivesButtonClick()
    {
        objectivesPanel.SetActive(!objectivesPanel.activeSelf);
    }
}
