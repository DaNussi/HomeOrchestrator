
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Led
{

    public Vector3 position;
    public Color color;
    public List<LedHandler> handlerStack;

    public Led(Vector3 position)
    {
        this.position = position;
        this.color = new Color(0,0,0,0);
        this.handlerStack = new List<LedHandler>();
    }

    public Led(Vector3 position, Color color)
    {
        this.position = position;
        this.color = color;
        this.handlerStack = new List<LedHandler>();
    }


}