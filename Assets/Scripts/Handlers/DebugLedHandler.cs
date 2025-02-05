using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class DebugLedHandler : LedHandler
{

    [SerializeField] private bool log = false;


    public override bool OnStart(List<Led> leds)
    {
        if(log) Debug.Log("Started");
        return true;
    }

    public override void OnStop(List<Led> leds)
    {
        if (log) Debug.Log("Stopped");
    }

    public override bool OnUpdate(List<Led> leds)
    {
        if (!log) return true;

        string message = string.Empty;

        for (int i = 0; i < leds.Count; i++)
        {
            message += "<color=#" + leds[i].color.ToHexString() + ">" + i + "</color>";
            if (i != leds.Count - 1) message += ", ";
        }


        Debug.Log(message);
        return true;
    }
}