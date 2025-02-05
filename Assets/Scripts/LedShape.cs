using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public abstract class LedShape : MonoBehaviour
{
    [SerializeField] public List<Led> leds = new List<Led>();
    [SerializeField] private List<Vector3> positions = new List<Vector3>();
    [SerializeField] private GameObject prefab;
    [SerializeField] public bool AutoUpdate = false;
    [SerializeField] private LedHandler handler;
    [SerializeField] private Dictionary<int, LedHandler> handlerOverwrite = new Dictionary<int, LedHandler>();

    public void Update()
    {
        if (AutoUpdate) UpdateLeds();
    }

    public void OnEnable()
    {
        UpdateLeds();
    }

    public void OnDisable()
    {
        while (leds.Count > 0)
        {
            DestroyLed(leds[0]);
        }
    }

    public void UpdateLeds()
    {
        if(prefab == null)
        {
            Debug.LogError("Prefab not set!", this);
            return;
        }

        leds = leds.Where(item => item != null).ToList();

        positions = GeneratePositions();
        
        while (positions.Count > leds.Count)
        {
            InstanciateLed();
        }

        while (leds.Count > positions.Count)
        {
            DestroyLed(leds[leds.Count - 1]);
        }

        if (leds.Count != positions.Count)
        {
            Debug.LogError("Missmatched array length when updating leds", this);
            return;
        }

        for (var i = 0; i < leds.Count; i++)
        {
            UpdateLed(leds[i], positions[i], handler);
        }
    }


    private void UpdateLed(Led led, Vector3 position, LedHandler handler)
    {
        led.transform.position = position;
        if (handler != null)
        {
            if (led.handler != null && led.handler != handler) Debug.LogWarning("Overwritten led handler", led);
            led.handler = handler;
        }
    }

    private void InstanciateLed()
    {
        var gameObject = Instantiate(prefab, this.transform);
        var led = gameObject.GetComponent<Led>();
        led.handler = handler;
        led.shape = this;
        leds.Add(led);
    }

    private void DestroyLed(Led led)
    {
        leds.Remove(led);
        led.handler = null;
#if UNITY_EDITOR
        DestroyImmediate(led.gameObject);
#else
        Destroy(led.gameObject);
#endif
    }

    public abstract List<Vector3> GeneratePositions();

    public List<Led> GetLeds()
    {
        return leds;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        if (positions.Count >= 1)
        {
            foreach (var led in leds)
            {
                Gizmos.DrawLine(this.transform.position, positions[0]);
            }

            foreach (var position in positions)
            {
                Gizmos.DrawWireCube(position, Vector3.one * 0.1f);
            }
        }

        if(positions.Count >= 2)
        {
            for (var i = 0; i < positions.Count - 1; i++)
            {
                Vector3 from = positions[i];
                Vector3 to = positions[i + 1];
                Gizmos.DrawLine(from, to);
            }
        }

    }


}
