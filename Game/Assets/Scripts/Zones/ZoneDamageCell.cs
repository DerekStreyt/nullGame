using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDamageCell : ZoneDamage
{
    public DebugCube link;
    protected override bool Validate(Character character)
    {
        return link.CanDamage();
    }

}
