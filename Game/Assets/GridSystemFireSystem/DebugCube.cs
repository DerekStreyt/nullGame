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

    public Vector2Int DebugCordIndexes;

    bool isFireResistant = false;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        render = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
            float scale = 0.5f;

            Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale * scale, transform.rotation);


            foreach (Collider col in colliders)
            {
                if(col.tag=="FireResistant")
                {
                     SetFireResistance(true);
                   //  Debug.Log("Setted fire resist    " + DebugCordIndexes);
                }

            }

        
    }

    public void SetFireResistance(bool value)
    {
        isFireResistant = value;

        if(CurrentCell!=null)
        {
            CurrentCell.IsFireResistant = true;
        }
    }

    public void SetCell(Cell c)
    {
        CurrentCell = c;
        DebugCordIndexes = CurrentCell.PositionIndex;
    }

    // Update is called once per frame

    public override bool ReceiveDamage(int damage,Vector3 worldPosition,Vector3 hitNormal)
    {
      //  Debug.Log("pos:" + worldPosition);
        bool result = false;
        if(CurrentCell!=null)
        {
            result = CurrentCell.FireDangerScale > 0;
            if (result)
            {
                GameManager.Instance.AddScore(CurrentCell.FireDangerScale);
            }
            CurrentCell.FireDangerScale = 0;
        }

        return result;
    }

    public virtual bool CanDamage()
    {
        if (CurrentCell != null)
        {
            //only big fire damage
            return CurrentCell.FireDangerScale > 5;
        }
        else
        {
            return false;
        }
    }

    public void SetCubeColor(Color c)
    {
       // render.material.color = c;
        render.material.SetColor("_BaseColor", c);
    }

    public void ShowFire(float intensity)
    {
        if (intensity >= 1f)
        {
            if (intensity>prevIntensity)
            {
                GameObject fire = null;
                prevIntensity = intensity;

                if(intensity>9f)
                {
                    fire = Storage.Instance.GetObject("FireAnimSquare_3");
                }
                else
                if (intensity > 7f)
                {
                    fire = Storage.Instance.GetObject("FireAnimSquare_2");
                }
                else
                if (intensity > 5f)
                {
                    fire = Storage.Instance.GetObject("FireAnimSquare_1");
                }
                else
                if (intensity > 3f)
                {
                    fire = Storage.Instance.GetObject("FireAnimSquare_0");
                }
                else
                {
                    //small
                    fire = Storage.Instance.GetObject("FireAnimSquare_Min");
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
