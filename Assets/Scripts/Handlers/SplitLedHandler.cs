
using System;
using System.Collections.Generic;
using UnityEngine;

class SplitLedHandler : LedHandler
{

    [SerializeField] private LedHandler handlerA;
    [SerializeField] private LedHandler handlerB;
    [SerializeField] private int splitIndex;

    public override void innerProcess(List<Led> leds)
    {

        handlerA.process(leds.GetRange(0, splitIndex));
        handlerB.process(leds.GetRange(splitIndex, leds.Count - splitIndex));

    
    }
}