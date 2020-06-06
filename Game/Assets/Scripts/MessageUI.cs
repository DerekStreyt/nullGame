using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageUI : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    protected RectTransform rectTransform;

    protected virtual void Awake()
    {
        rectTransform = (RectTransform)transform;
    }

    protected virtual void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(target.position+ offset);
        rectTransform.position = pos;
    }
}
