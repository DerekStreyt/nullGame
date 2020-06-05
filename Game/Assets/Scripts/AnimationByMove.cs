using UnityEngine;

public class AnimationByMove : MonoBehaviour
{
    public Animator animator;
    public float maxSpeed = 3;
    public float lerpFactor = 4;
    protected Vector3 lastPosition;
    protected float moveValue01;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
        lastPosition.y = 0;
    }

    protected virtual void Update()
    {
        Vector3 currentPosition = transform.position;
        lastPosition.y = 0;
        currentPosition.y = 0;

        float v = Vector3.Distance(lastPosition, currentPosition);
        float speed = v / Time.deltaTime;
        float targetValue = Mathf.Abs(speed) < float.Epsilon ? 0 : speed / maxSpeed;
        moveValue01 = Mathf.Lerp(moveValue01, targetValue, lerpFactor * Time.smoothDeltaTime);
        animator.SetFloat("Speed", moveValue01);
        lastPosition = currentPosition;
    }
}
