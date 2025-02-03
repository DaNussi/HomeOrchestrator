using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class MonoLedShape : LedShape
{
    [SerializeField] private Color defaultColor = Color.black;

    public override List<Led> getLeds()
    {
        var position = this.transform.position;

        var led = new Led(position, defaultColor);

        return new List<Led> { led };
    }
}