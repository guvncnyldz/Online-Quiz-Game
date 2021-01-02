using System;
using Newtonsoft.Json.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using WebSocketSharp;

public class SocketClient : MonoBehaviour
{
    WebSocket ws;
    public string id;

    private void Start()
    {
        ws = new WebSocket("ws://localhost:3000");
        ws.ConnectAsync();
        
        
        ws.OnOpen += (sender, args) =>
        {
            JObject jObject = new JObject();
            jObject.Add("method","joinroom");
            ws.Send(jObject.ToString());
        };

        ws.OnClose += (sender, args) =>
        {
            Debug.Log("Bağlantı koptu");
        };
        

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log(id + " " +e.Data);
        };
    }

    private void Update()
    {
        if (ws == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JObject jObject = new JObject();
            jObject.Add("method","myinfo");
            jObject.Add("id",id);
            ws.Send(jObject.ToString());
        }
        if (Input.GetKeyDown(KeyCode.A) && id == "a")
        {
            Application.Quit();
        }
    }
}