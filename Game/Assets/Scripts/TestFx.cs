using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFx : MonoBehaviour
{
    public ParticleSystem fx;
    public Transform target;

    public virtual void OnGUI()
    {
        GUILayout.Space(400);
        if (GUILayout.Button("<<<"))
        {
            fx.Emit(new ParticleSystem.EmitParams()
            {
                position = target.position,
            }, 1);
        }
    }
}
