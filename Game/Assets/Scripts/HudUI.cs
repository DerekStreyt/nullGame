using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : UnityPoolObject
{
    public Text text;

    protected float timer;
    protected float offset = 0;
    protected Vector3 target;
    protected RectTransform rectTransform;

    protected override void Awake()
    {
        base.Awake();
        rectTransform = (RectTransform) transform;
    }

    protected virtual void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(target);
        pos.y += offset;
        rectTransform.position = pos;
        if (timer > GameConfig.Instance.hudTime)
        {
            timer = 0;
            offset = 0;
            Push();
        }
        else
        {
            timer += Time.deltaTime;
            offset += GameConfig.Instance.hudSpeed * Time.deltaTime;
        }
    }

    public virtual void Attach(Vector3 target, string str, Color color)
    {
        this.target = target;
        text.text = str;
        text.color = color;
    }
}
