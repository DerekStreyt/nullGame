using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{ 
    public virtual void Apply()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
