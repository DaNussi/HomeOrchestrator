using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LedHandler : MonoBehaviour
{

    public void process(List<Led> leds)
    {
        leds.ForEach(l => l.handlerStack.Add(this));
        innerProcess(leds);
    }

    public abstract void innerProcess(List<Led> leds);

    

}
