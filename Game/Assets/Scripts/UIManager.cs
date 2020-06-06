using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button objectivesButton;
    public GameObject objectivesPanel;
    public Button closeButton;
    
    // Start is called before the first frame update
    void Start()
    {
        objectivesButton.onClick.AddListener(OnObjectivesButtonClick);
        closeButton.onClick.AddListener(OnObjectivesButtonClick);
    }

    private void OnObjectivesButtonClick()
    {
        objectivesPanel.SetActive(!objectivesPanel.activeSelf);
    }
}
