using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "Datas/ItemData")]
public class ItemData : ScriptableObject
{
    public Item[] items;
}

[Serializable]
public class Item
{
    public string id;
    public string name;
    public Sprite sprite;
    public int moneyCost;
    public int goldCost;
    public CosmeticTypes cosmeticTypes;
    public bool isDefault;
    public bool isFree;
}

[Serializable]
public enum CosmeticTypes
{
    Head,
    Hair,
    Eye,
    Body,
    Hand,
    Foot
}