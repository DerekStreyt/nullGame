using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAutoPitch : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource source;

    void Awake()
    {
       source= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (source != null)
        {
            //change pitch randomly
            float pitch =Mathf.Clamp(1f + Mathf.Sin(Time.time*0.5f)*0.3f,1f,2f);
            source.pitch = pitch;
        }

    }
}
