using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class DebugLedHandler : LedHandler
{

    [SerializeField] private bool logProcess = false;

    public override void innerProcess(List<Led> leds)
    {

        string message = string.Empty;

        for (int i = 0; i < leds.Count; i++)
        {
            message += "<color=#" + leds[i].color.ToHexString()+">" +i+"</color>";
            if( i != leds.Count -1 ) message += ", ";
        }


        Debug.Log(message);
    }
}