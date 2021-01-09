using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class MarketData : MonoBehaviour
{
    public static MarketData instance;

    public List<JToken> items;

    private void Awake()
    {
        instance = this;
    }

    public void SetData(JArray jArray)
    {
        items = new List<JToken>();

        foreach (JToken token in jArray)
        {
            items.Add(token);
        }
    }

    public List<JToken> GetData(CosmeticTypes type)
    {
        List<JToken> choosenItems = new List<JToken>();

        foreach (JToken item in items)
        {
            if (item["type"].ToString() == type.ToString())
            {
                choosenItems.Add(item);
            }
        }

        return choosenItems;
    }
}