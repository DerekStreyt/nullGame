using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class ZoneBase<T> : MonoBehaviour where T: UnitWithHealth
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        T unit = other.GetComponent<T>();
        if (unit != null)
        {
            OnEnter(unit);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        T unit = other.GetComponent<T>();
        if (unit != null)
        {
            OnExit(unit);
        }
    }

    protected abstract void OnEnter(T unit);
    protected abstract void OnExit(T unit);
}
