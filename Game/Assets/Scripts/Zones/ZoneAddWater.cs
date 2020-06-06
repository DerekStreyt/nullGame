using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAddWater : ZoneBase<Unit>
{
    protected float timer = 0;
    protected Unit unit;
    protected virtual void Update()
    {
        if (unit != null)
        {
            if (timer > GameConfig.Instance.addWaterTime)
            {
                unit.AddWater(GameConfig.Instance.addWater);
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    protected override void OnEnter(Unit unit)
    {
        this.unit = unit;
    }

    protected override void OnExit(Unit unit)
    {
        this.unit = null;
    }
}
