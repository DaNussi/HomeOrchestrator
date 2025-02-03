

using System.Collections.Generic;
using UnityEngine;

class LedController : MonoBehaviour
{

    [SerializeField] private LedShape shape;
    [SerializeField] private LedHandler handler;
    [SerializeField] private Color color;

    public List<Led> leds = new List<Led>();
    

    public void Update()
    {
        
        leds = shape.getLeds();
        leds.ForEach(l => l.color = this.color);
        handler.process(leds);

    }

    public void OnDrawGizmos()
    {
        
        foreach (Led led in leds)
        {
            Gizmos.color = led.color;
            Gizmos.DrawWireSphere(led.position, 0.1f);
        }

    }


}
