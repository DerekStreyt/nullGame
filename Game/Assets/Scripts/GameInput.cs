using UnityEngine;

public class GameInput : MonoBehaviour
{
    public Camera rayCamera;
    public LayerMask mask;
    public Unit unit;

    protected virtual void Update()
    {
        unit.Move (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 10000, mask.value))
        {
            unit.target = hit.point;
        }
        else
        {
            unit.target = unit.Position + unit.transform.forward;
        }

        if (Input.GetMouseButton(0))
        {
            unit.SetForce(1);
        }
        if (Input.GetMouseButton(1))
        {
            unit.SetForce(-1);
        }
    }
}
