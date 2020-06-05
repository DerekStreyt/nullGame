using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int waterVertexCount = 50;
    
    public int damage = 1;
    public float maxDistance = 10;
    public Vector3 Position => transform.position;
    public float rotateSpeed = 5;
    public Vector3 target;
    public LayerMask waterMask;
    public ParticleSystem defaultWaterFx;
    public ParticleSystem fireWaterFx;

    public float hFactor = 0.05f;
    public float minForce = 0.5f;
    public float maxForce = 10f;
    public float force = 1;
    public float forceStep = 0.2f;

    public LineRenderer waterRenderer;
    public Transform waterPivot;
    public float gravity;

    public float offsetTime;
    public float offsetRadius;
    public float waterMoveSpeed = 2;

    private Vector3 waterTarget;

    private Vector3 offset;
    private float offsetTimer;


    protected virtual void Start()
    {
        force = minForce;
    }

    protected virtual void Update()
    {
        Vector3 lookAt = GetLookPoint() - Position;
        if (lookAt.sqrMagnitude > 0.01)
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookAt);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
        }

        RandomOffset();
        UseWater();
    }

    public virtual void SetForce(float direction)
    {
        force += direction * forceStep * Time.deltaTime;
        force = Mathf.Clamp(force, minForce, maxForce);
    }

    protected virtual Vector3 GetLookPoint()
    {
        return new Vector3(target.x, Position.y, target.z);
    }

    protected virtual void UseWater()
    {
        Vector3 newTarget = target;

        float distanceToTarget = Vector3.Distance(Position, newTarget);

        if (distanceToTarget > maxDistance)
        {
            Vector3 dir = newTarget - Position;
            dir.Normalize();
            newTarget = Position + dir * maxDistance;
            Debug.DrawRay(newTarget, Vector3.up * 10, Color.magenta);
            distanceToTarget = maxDistance;
        }

       
        waterTarget = Vector3.Lerp(waterTarget, newTarget + offset, waterMoveSpeed * Time.deltaTime);
        
        Vector3 prev = waterPivot.position;
        waterRenderer.positionCount = waterVertexCount;
        waterRenderer.SetPosition(0, prev);
        Debug.DrawRay(prev, Vector3.up, Color.green);
        Debug.DrawRay(newTarget, Vector3.up, Color.red);
        Vector3 direction = (waterTarget - prev);
        direction.y = 0;
        Vector3 forceVector = direction;
        
        float distance = Vector3.Distance(prev, newTarget);
        Vector3 gravityVector  = new Vector3(0, distance * hFactor, 0); // todo
        for (int i = 1; i < waterVertexCount; i++)
        {
            Vector3 next = prev + forceVector * (force * Time.fixedDeltaTime) + gravityVector;
            gravityVector -= Vector3.up * gravity;
        
            Debug.DrawLine(prev, next);
            if (Physics.Linecast(prev, next, out var hit, waterMask.value))
            {
                Debug.DrawRay(hit.point, Vector3.up * 10, Color.red);
                Debug.DrawRay(hit.point, Vector3.right * 10, Color.red);
                Debug.DrawRay(hit.point, Vector3.forward * 10, Color.red);
                ApplyWaterFx(hit.point, Vector3.up);

                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.ReceiveDamage(damage);
                    ApplyFireWaterFx(hit.point, Vector3.up);
                }
                else
                {
                    ApplyFireWaterFx(new Vector3(1000, 0, 0), Vector3.up);
                }

                waterRenderer.SetPosition(i, hit.point);
                waterRenderer.positionCount = i + 1;
                break;
            }
            else
            {
                waterRenderer.SetPosition(i, next);
            }
            prev = next;
        }
    }

    protected virtual void RandomOffset()
    {
        if (offsetTimer > offsetTime)
        {
            offsetTimer = 0;
            Vector2 r = Random.insideUnitCircle;
            offset = new Vector3(r.x * offsetRadius, 0 , r.y * offsetRadius);

        }
        else
        {
            offsetTimer += Time.deltaTime;
        }
    }

    protected virtual void ApplyWaterFx(Vector3 position, Vector3 normal)
    {
        defaultWaterFx.transform.position = position;
      /*  defaultWaterFx.Emit(new ParticleSystem.EmitParams()
        {
            position = position
        }, 1);*/
    }

    protected virtual void ApplyFireWaterFx(Vector3 position, Vector3 normal)
    {
        fireWaterFx.transform.position = position;
        /*  defaultWaterFx.Emit(new ParticleSystem.EmitParams()
          {
              position = position
          }, 1);*/
    }
}
