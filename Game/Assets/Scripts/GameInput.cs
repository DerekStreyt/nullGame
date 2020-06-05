using System;
using UnityEngine;
using UnityEngine.AI;

public class GameInput : MonoBehaviour
{
    public enum EState
    {
        WASD,
        Point
    }

    public Camera rayCamera;
    public LayerMask mask;
    public EState state;
    public CharacterController characterController;
    public NavMeshAgent agent;
    public float speed = 3.5f;
    public Vector3 moveDirection;

    protected virtual void Start()
    {
        agent.speed = speed;
    }

    private void OnGUI()
    {
        if (GUILayout.Button(state == EState.WASD ? "WASD" : "POINT"))
        {
            if (state == EState.Point)
            {
                state = EState.WASD;
            }
            else
            {
                state = EState.Point;
            }
        }
    }

    protected virtual void Update()
    {
     
        switch (state)
        {
            case EState.WASD:
                ApplyWASDMove();
                break;
            case EState.Point:
                ApplyPointMove();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

    }

    private void ApplyWASDMove()
    {
        agent.enabled = false;
        characterController.enabled = true;
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection.Normalize();
        characterController.transform.LookAt(characterController.transform.position + moveDirection);
        moveDirection *= speed;
        moveDirection.y = -1;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void ApplyPointMove()
    {
        agent.enabled = true;
        characterController.enabled = false;
        if (Input.GetMouseButton(0))
        {
            Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, mask))
            {
                agent.destination = hit.point;
            }
        }

        if (!agent.isStopped)
        {
            Vector3 direction = agent.velocity;
            direction.Normalize();
            direction.y = 0;
            agent.transform.LookAt(agent.transform.position + direction);
        }
    }
}
