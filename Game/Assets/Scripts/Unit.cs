using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int waterVertexCount = 50;
    public Vector3 Position => transform.position;
    public float rotateSpeed = 5;
    public Vector3 target;
    public Vector3 startVector = Vector3.up;
    public float force = 5;
    public float forceY = 3;
    public LineRenderer waterRenderer;
    public Transform waterPivot;
    public float gravity;

    protected virtual void Update()
    {
        Vector3 lookAt = GetLookPoint() - Position;
        if (lookAt.sqrMagnitude > 0.01)
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookAt);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        }

        UseWater();
    }

    protected virtual Vector3 GetLookPoint()
    {
        return new Vector3(target.x, Position.y, target.z);
    }

    protected virtual void UseWater()
    {
        Vector3 prev = waterPivot.position;
        waterRenderer.positionCount = waterVertexCount;
        waterRenderer.SetPosition(0, prev);
        Debug.DrawRay(prev, Vector3.up, Color.green);
        Debug.DrawRay(target, Vector3.up, Color.red);
        Vector3 direction = (target - prev);
        direction.y = 0;
        Vector3 forceVector = direction + Vector3.up * forceY * Time.fixedDeltaTime;
        
        float distance = Vector3.Distance(prev, target);
        Vector3 gravityVector  = new Vector3(0, distance * 0.1f, 0); // todo
        for (int i = 1; i < waterVertexCount; i++)
        {
            Vector3 next = prev + forceVector * (force * Time.fixedDeltaTime) + gravityVector;
            gravityVector -= Vector3.up * gravity;
            waterRenderer.SetPosition(i, next);
            Debug.DrawLine(prev, next);
            prev = next;
        }
    }
}
