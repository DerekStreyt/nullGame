﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Unit : Character
{
    public int waterVertexCount = 50;

    public float maxWater = 100;
    public float currentWater = 100;
    public float waterDecrementSpeed = 0.5f;

    public event Action<float> onWaterChange;

    public float physForce = 10;
    public float speed = 3.5f;
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
    public AudioSource waterSound;
    public float gravity;

    public float offsetTime;
    public float offsetRadius;
    public float waterMoveSpeed = 2;


    private Vector3 waterTarget;

    private Vector3 offset;
    private float offsetTimer;


    private float CurrentWater
    {
        get
        {
            return currentWater;
        }
        set
        {
            currentWater = value;
            onWaterChange?.Invoke(currentWater);
        }
    }

    protected virtual void Start()
    {
        force = minForce;
    }

    protected virtual void Update()
    {
        if (IsActive)
        {
            Vector3 lookAt = GetLookPoint() - Position;
            if (lookAt.sqrMagnitude > 0.01)
            {
                Quaternion lookRotation = Quaternion.LookRotation(lookAt);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
            }

            UseWater();
        }
    }

    public virtual void AddWater(float water)
    {
        if (IsActive)
        {
            
            if (CurrentWater + water> maxWater)
            {
                GameManager.Instance.CreateWaterHud(maxWater - CurrentWater);
                CurrentWater = maxWater;
            }
            else
            {
                CurrentWater += water;
                GameManager.Instance.CreateWaterHud(water);
            }
            
        }
    }

    public virtual void Move(Vector3 direction)
    {
        if (IsActive)
        {
            Vector3 moveDirection = direction.normalized;
            moveDirection *= speed;
            moveDirection.y = -1;
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    public virtual void SetForce(float direction)
    {
        if (IsActive)
        {
            force += direction * forceStep * Time.deltaTime;
            force = Mathf.Clamp(force, minForce, maxForce);
        }
    }

    protected override void OnDie()
    {
        base.OnDie();
        force = 0;
        GameManager.Instance.Lose();
    }

    protected virtual Vector3 GetLookPoint()
    {
        return new Vector3(target.x, Position.y, target.z);
    }

    protected virtual void UseWater()
    {
        if (CurrentWater <= 0 || force <= minForce)
        {
            waterRenderer.positionCount = 0;
            force = minForce;
            ApplyWaterFx(new Vector3(1000, 0, 0), Vector3.up);
            waterSound.Pause();
            return;
        }

        if (!waterSound.isPlaying)
        {
            waterSound.Play();
        }


        CurrentWater -= force * waterDecrementSpeed * Time.deltaTime;

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
        if (offsetTimer > offsetTime)
        {
            offsetTimer = 0;
            Vector2 r = Random.insideUnitCircle;
            float factor = Mathf.InverseLerp(0, maxDistance, distanceToTarget);
            float currentRadius = offsetRadius * factor;
            offset = new Vector3(r.x * currentRadius, 0, r.y * currentRadius);
        }
        else
        {
            offsetTimer += Time.deltaTime;
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

                DestructibleObject enemy = hit.collider.GetComponent<DestructibleObject>();
                if (enemy != null)
                {
                    if (enemy.ReceiveDamage(damage, hit.point, hit.normal))
                    {
                        ApplyFireWaterFx(hit.point, Vector3.up);
                    }
                }

                Bonus bonus = hit.collider.GetComponent<Bonus>();
                if (bonus != null)
                {
                    bonus.Apply();
                }

                Rigidbody rigidbody = hit.collider.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.AddForceAtPosition(direction.normalized * force * physForce, hit.point, ForceMode.Force);
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

    protected virtual void ApplyWaterFx(Vector3 position, Vector3 normal)
    {
        defaultWaterFx.transform.position = position;
    }

    protected virtual void ApplyFireWaterFx(Vector3 position, Vector3 normal)
    {
        //  fireWaterFx.transform.position = position;
        fireWaterFx.Emit(new ParticleSystem.EmitParams()
          {
              position = position
          }, 1);
    }

    public override bool ReceiveDamage(int damage, Vector3 position, Vector3 normal)
    {
        bool result = false;
        if (IsActive)
        {
            GameManager.Instance.CreateDamageHud(damage);
            result = base.ReceiveDamage(damage,position,normal);
        }

        return result;
    }
}
