using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWater : MonoBehaviour
{
    public int count = 10;
    public float force = 10;
    public float forceY = 3;
    public Vector3 startVector = Vector3.up;
    public float gravity = 0.98f;
    public Transform from;
    public Transform to;
    public LineRenderer lineRenderer;

    void Update()
    {
        lineRenderer.positionCount = count;

        Vector3 direction = (to.position - @from.position);
        Debug.DrawRay(@from.position, direction * 10, Color.green);
        lineRenderer.SetPosition(0, @from.position);
        Vector3 prev = @from.position;
        Vector3 gravityVector = startVector;

        Vector3 forceVector = direction + Vector3.up * forceY * Time.fixedDeltaTime;
        for (int i = 1; i < count; i++)
        {

            Vector3 next = prev + forceVector * (force * Time.fixedDeltaTime) + gravityVector;
            gravityVector -= Vector3.up * gravity;
            lineRenderer.SetPosition(i, next);
            Debug.DrawLine(prev, next);
            prev = next;
        }
        
        /*
        Vector3 a = Vector3.zero;
        Vector3 v = Vector3.right;
        Vector3 up = Vector3.up;
        Vector3 g = up;
        Vector3 prev1 = a;
        for (int i = 1; i < count; i++)
        {

            Vector3 right = v + Vector3.up * forceY;

            Vector3 next = prev1 + right * (force * Time.fixedDeltaTime) + g;

            g -= Vector3.up;
            
            Debug.DrawLine(prev1, next);
            Debug.DrawRay(prev1, Vector3.up, Color.cyan);
            prev1 = next;
        }*/
    }
}
