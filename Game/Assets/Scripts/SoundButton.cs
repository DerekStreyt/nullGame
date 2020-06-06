using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public AudioClip soundOnClick;
    public AudioSource audioSource;
    public Button button;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = soundOnClick;
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        audioSource.Play();
    }
}
