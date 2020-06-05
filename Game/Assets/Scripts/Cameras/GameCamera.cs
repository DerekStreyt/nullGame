using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{
    public Camera currentCamera;
    public Transform target;

    public float distance = 5.0f;

    public float minDistance = 5f;
    public float maxDistance = 20;

    public float ySpeed = 200.0f;

    public int yBegin = 60;
    public int yMinLimit = 30;
    public int yMaxLimit = 90;

    public int zoomSpeed = 40;
    public float zoomDampening = 5.0f;

    public Vector3 offset;
    public bool isActive;


    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Transform myTransform;

    // --------------------------------------------------------------------
    private void Awake()
    {
        myTransform = transform;
    }

    // --------------------------------------------------------------------
    private void Start()
    {
        Init();
        isActive = true;
    }

    // --------------------------------------------------------------------
    private void Update()
    {
        if (!isActive || target == null)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            yDeg -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);

            desiredRotation = Quaternion.Euler(yDeg, 0, 0);
            currentRotation = myTransform.rotation;

            rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
            myTransform.rotation = rotation;
        }

        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed * Mathf.Abs(desiredDistance);

        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);

        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);

        myTransform.position = target.position - (rotation * Vector3.forward * currentDistance) + offset;
    }

    // --------------------------------------------------------------------
    public void Init()
    {
        currentDistance = distance;
        desiredDistance = distance;

        yDeg = Vector3.Angle(Vector3.up, myTransform.up);

        rotation = Quaternion.Euler(new Vector3(yBegin, 0, 0));
        transform.rotation = rotation;
        currentRotation = myTransform.rotation;
        desiredRotation = myTransform.rotation;
    }

    // --------------------------------------------------------------------
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
