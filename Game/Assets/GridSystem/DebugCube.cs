using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCube : MonoBehaviour
{
    MeshRenderer render;
    // Start is called before the first frame update
    void Awake()
    {
        render = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCubeColor(Color c)
    {
       // render.material.color = c;
        render.material.SetColor("_BaseColor", c);
    }


    private void OnMouseDown()
    {
        WorldCellSystem.Instance.ApplyWater(transform.position,5f);
    }
}
