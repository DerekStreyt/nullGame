using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cell = WorldCellSystem.Cell;

public class DebugCube : DestructibleObject
{
    MeshRenderer render;
    private Cell CurrentCell;

    private GameObject currentFireEffect;

    bool isFireShowing = false;

    Vector3 effectOffset = new Vector3(0f, 0.5f, 0f);

    float prevIntensity = 0f;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        render = GetComponent<MeshRenderer>();
    }

    public void SetCell(Cell c)
    {
        CurrentCell = c;
    }

    // Update is called once per frame

    public override bool ReceiveDamage(int damage,Vector3 worldPosition,Vector3 hitNormal)
    {
      //  Debug.Log("pos:" + worldPosition);
        bool result = false;
        if(CurrentCell!=null)
        {
            result = CurrentCell.FireDangerScale > 0;
            CurrentCell.FireDangerScale = 0;
        }

        return result;
    }

    public virtual bool CanDamage()
    {
        return CurrentCell.FireDangerScale > 3;
    }

    public void SetCubeColor(Color c)
    {
       // render.material.color = c;
        render.material.SetColor("_BaseColor", c);
    }

    public void ShowFire(float intensity)
    {
        if (intensity > 5f)
        {
            if (intensity>prevIntensity)
            {
                GameObject fire = null;
                prevIntensity = intensity;

                if (intensity>7f)
                {
                    fire = Storage.Instance.GetObject("FireAnimSquare_2");
                }else if(intensity>9f)
                {
                    fire = Storage.Instance.GetObject("FireAnimSquare_3");
                }
                else
                {
                    //small
                    fire = Storage.Instance.GetObject("FireAnimSquare_1");
                }

                

                if (fire != null)
                {
                    //remove previous fire
                    if (currentFireEffect!=null)
                    {
                        Destroy(currentFireEffect);
                        currentFireEffect = null;
                    }

                    if (currentFireEffect == null)
                    {
                        currentFireEffect = Instantiate(fire, transform.position + effectOffset, Quaternion.identity);
                    }
                    else
                    {
                        currentFireEffect.SetActive(true);
                    }
                }


                isFireShowing = true;
              
            }
        }
        else
        {
            if(currentFireEffect!=null)
            {
                currentFireEffect.SetActive(false);
            }
        }
    }

}
