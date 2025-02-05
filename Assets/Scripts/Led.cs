
using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Led : MonoBehaviour
{

    [SerializeField] public Color color = Color.black;
    [SerializeField] public LedShape shape;
    [SerializeField] public LedHandler handler;
    private LedHandler oldHandler;

    public void Update()
    {
        if(oldHandler != handler)
        {
            if(oldHandler != null) oldHandler.unregister(this);
            handler.register(this);
            oldHandler = handler;
            Debug.Log("Updated Handler");
        }

        if (handler == null) Debug.LogWarning("Led missing handler", this);
    }

    public void OnDestroy()
    {
        if(handler != null) handler.unregister(this);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(this.transform.position, 0.1f);


        Color.RGBToHSV(color, out float h, out float s, out float v);
        Color current = Color.HSVToRGB(h, s, v);
        Color opposite = Color.HSVToRGB((h + 0.5f) % 1f, s, v);

        Gizmos.color = Color.Lerp(current, opposite, color.a);
        Gizmos.DrawWireSphere(this.transform.position, 0.1f);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if(handler != null) Gizmos.DrawLine(this.transform.position, this.handler.transform.position);
    }

}