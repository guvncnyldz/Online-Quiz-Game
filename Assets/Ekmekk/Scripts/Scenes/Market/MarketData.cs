using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class MarketData : MonoBehaviour
{
    public static MarketData instance;

    [SerializeField] private ItemData itemData;

    private void Awake()
    {
        instance = this;
    }

    public List<Item> GetData(CosmeticTypes type)
    {
        List<Item> items = new List<Item>();

        foreach (Item item in itemData.items)
        {
            if (item.cosmeticTypes == type && !item.isDefault && !item.isFree)
            {
                items.Add(item);
            }
        }

        return items;
    }
}