using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDamage : ZoneBase<Character>
{
    protected float timer = 0;
    protected HashSet<Character> characters = new HashSet<Character>();

    protected virtual void Update()
    {
        if (timer > GameConfig.Instance.damageTime)
        {
            foreach (var character in characters)
            {
                if (Validate(character))
                {
                    character.ReceiveDamage(GameConfig.Instance.damageByZone, Vector3.zero, Vector3.zero);
                }
            }
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    protected virtual bool Validate(Character character)
    {
        return true;
    }

    protected override void OnEnter(Character unit)
    {
        characters.Add(unit);
    }

    protected override void OnExit(Character unit)
    {
        characters.Remove(unit);
    }
}
