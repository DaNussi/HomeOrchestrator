using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
class SimpleLedEffect : MonoBehaviour
{
    [SerializeField] private List<LedShape> shapes = new List<LedShape>();
    [SerializeField] private Color color = Color.HSVToRGB(0f, 1f, 1f);
    [SerializeField, Range(0, 1)] private float Hue = 0;
    [SerializeField, Range(0, 1)] private float Saturation = 0;
    [SerializeField, Range(0, 1)] private float Value = 0;
    private Color oldColor;

    private void Update()
    {
        if (oldColor != color)
        {
            oldColor = color;
            Color.RGBToHSV(color, out Hue, out Saturation, out Value);
        } else
        {
            color = Color.HSVToRGB(Hue, Saturation, Value);
        }

        foreach (var shape in shapes)
        {
            if (shape == null) continue;
            foreach (var led in shape.GetLeds())
            {
                if (led == null) continue;
                led.color = color;
            }
        }
    }


}