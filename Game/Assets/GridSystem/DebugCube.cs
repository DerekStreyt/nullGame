using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell = WorldCellSystem.Cell;

public class DebugCube : DestructibleObject
{
    MeshRenderer render;
    private Cell CurrentCell;
    
    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake();
        render = GetComponent<MeshRenderer>();
    }

    public void SetCell(Cell c)
    {
        CurrentCell = c;
    }

    // Update is called once per frame

    public override void ReceiveDamage(int damage,Vector3 worldPosition,Vector3 hitNormal)
    {
        Debug.Log("pos:" + worldPosition);


        if(CurrentCell!=null)
        {
            CurrentCell.FireDangerScale = 0;
        }

    }

    public void SetCubeColor(Color c)
    {
       // render.material.color = c;
        render.material.SetColor("_BaseColor", c);
    }


    //private void OnMouseDown()
    //{
    //    WorldCellSystem.Instance.ApplyWater(transform.position,5f);
    //}
}
