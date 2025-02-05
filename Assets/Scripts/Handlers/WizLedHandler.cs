
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

class WizLedHandler : LedHandler
{


    [SerializeField] private string host = "192.168.0.0";
    [SerializeField] private int port = 38899;

    private UdpClient udpClient = new UdpClient();
    private bool isReady = false;


    private void sendColor(Color color)
    {
        if (!isConnected()) return;

        int r = (int)Math.Round(color.r * 255, 0);
        int g = (int)Math.Round(color.g * 255, 0);
        int b = (int)Math.Round(color.b * 255, 0);
        int a = (int)Math.Round(color.a * 255, 0);

        bool isOff = a == 0 || (r == 0 && g == 0 && b == 0);
        string state = !isOff ? "true" : "false";

        string data = "{\"id\":1,\"method\":\"setPilot\",\"params\":{\"r\":"+r+",\"g\":"+g+",\"b\":"+b+",\"dimming\":"+a+",\"state\":"+ state+"}}";
        Byte[] bytes = Encoding.ASCII.GetBytes(data);
        udpClient.Send(bytes, bytes.Length);
    }

    public bool isConnected()
    {
        if (udpClient.Client == null) return false;
        if (!udpClient.Client.IsBound) return false;
        return true;
    }

    public override bool OnStart(List<Led> leds)
    {
        if (leds.Count != 1)
        {
            Debug.LogError("Wiz led handler can only handle one led!", this);
            return false;
        }

        udpClient.Connect(host, port);
        sendColor(Color.black);
        this.isReady = true;
        Debug.Log("Connected " + host, this);
        return isConnected();
    }

    public override void OnStop(List<Led> leds)
    {
        this.isReady = false;
        try
        {
            sendColor(new Color(0, 0, 0, 0));
            udpClient.Close();
        } catch (Exception e)
        {
            Debug.LogWarning(e);
        }
        udpClient = new UdpClient();
        Debug.Log("Disconnected " + host, this);
    }

    public override bool OnUpdate(List<Led> leds)
    {
        if (leds.Count != 1)
        {
            Debug.LogError("Wiz led handler can only handle one led!", this);
            return false;
        }

        if (!isConnected())
        {
            Debug.LogWarning("Udp client not connected!", this);
            return false;
        }

        if (!isReady)
        {
            Debug.LogWarning("Not ready!", this);
            return false;
        }


        Led led = leds[0];

        sendColor(led.color);
        return isConnected();
    }
}