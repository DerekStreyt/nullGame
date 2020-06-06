using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTree : MonoBehaviour
{
    public float time = 1;
    public Vector3 offset = Vector3.up;

    protected float timer = 0;
    protected DestructibleObject fire;

    protected virtual void Update()
    {
        if (fire == null)
        {
            if (timer > time)
            {
                timer = 0;
                WorldCellSystem.Cell cell = WorldCellSystem.Instance.GetCell(transform.position);
                if (cell.FireDangerScale >= 4)
                {
                    fire = UnityPoolManager.Instance.PopOrCreate(GameManager.Instance.fireFxPrefab);
                    fire.transform.position = transform.position + offset;
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else if(!fire.IsActive)
        {
            fire = null;
        }
    }

}
