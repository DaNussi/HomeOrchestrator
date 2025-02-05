using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public abstract class LedHandler : MonoBehaviour
{
    [SerializeField] private List<Led> leds = new List<Led>();

    public void register(Led led)
    {
        leds.Add(led);
    }

    public void unregister(Led led)
    {
        leds.Remove(led);
    }

    private bool isRunning = false;

    public void Update()
    {
        leds = leds.Where(led => led != null).Distinct().ToList();

        if (!isRunning)
        {
            var success = OnStart(leds);
            isRunning = success;
            if (!success) OnStop(leds);
        }


        if(isRunning)
        {
            var success = OnUpdate(leds);
            isRunning = success;
            if (!success) OnStop(leds);
        }

    }

    public void OnDestroy()
    {
        isRunning = false;
        OnStop(leds);
    }


    public abstract bool OnStart(List<Led> leds);

    public abstract void OnStop(List<Led> leds);

    public abstract bool OnUpdate(List<Led> leds);


}
