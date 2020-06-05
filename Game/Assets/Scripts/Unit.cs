using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector3 Position => transform.position;
    public float rotateSpeed = 5;
    public Vector3 target;

    protected virtual void Update()
    {
        Vector3 lookAt = GetLookPoint() - Position;
        if (lookAt.sqrMagnitude > 0.01)
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookAt);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        }
    }
    protected virtual Vector3 GetLookPoint()
    {
        return new Vector3(target.x, Position.y, target.z);
    }
}
