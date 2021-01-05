﻿using UnityEngine;
using WebSocketSharp;

public static class SocketUtil
{
    private const string URL = "ws://192.168.1.34:8000";

    public static WebSocket ws;

    public static void Connect()
    {
        if (ws == null)
        {
            ws = new WebSocket(URL);
        }

        if (!ws.IsAlive)
        {
            ws.ConnectAsync();
        }

        /*ws.OnClose += (sender, args) =>
        {
            Debug.Log("tekrar deneniyor");
            ws.ConnectAsync();
        };*/
    }
}