﻿using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventorySystem : MonoBehaviour
{
    public List<InventoryCosmetic> cosmetics;

    public InventorySystem()
    {
        cosmetics = new List<InventoryCosmetic>();
    }

    public async void GetInventory(string userId)
    {
        var values = new Dictionary<string, string>
        {
            {"user_id", userId},
        };

        JArray response = await HTTPApiUtil.Post(values, "inventory", "getinventory");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
        }

        foreach (JToken jToken in response)
        {
            InventoryCosmetic inventoryCosmetic = new InventoryCosmetic();
            inventoryCosmetic.sprite_name = jToken["sprite_name"].ToString();
            inventoryCosmetic.name = jToken["name"].ToString();

            switch (jToken["type"].ToString())
            {
                case "Hair":
                    inventoryCosmetic.type = CosmeticTypes.Hair;
                    break;
                case "Hand":
                    inventoryCosmetic.type = CosmeticTypes.Hand;
                    break;
                case "Foot":
                    inventoryCosmetic.type = CosmeticTypes.Foot;
                    break;
                case "Body":
                    inventoryCosmetic.type = CosmeticTypes.Body;
                    break;
                case "Eye":
                    inventoryCosmetic.type = CosmeticTypes.Eye;
                    break;
            }

            cosmetics.Add(inventoryCosmetic);
        }
    }

    public bool CheckExist(string sprite_name)
    {
        foreach (InventoryCosmetic inventory in cosmetics)
        {
            if (inventory.sprite_name == sprite_name)
                return true;
        }

        return false;
    }

    public void AddCosmetic(string name, string spriteName, CosmeticTypes type)
    {
        InventoryCosmetic inventoryCosmetic = new InventoryCosmetic();
        inventoryCosmetic.sprite_name = spriteName;
        inventoryCosmetic.type = type;
        inventoryCosmetic.name = name; 
        
        cosmetics.Add(inventoryCosmetic);
    }
}

public class InventoryCosmetic
{
    public string sprite_name;
    public CosmeticTypes type;
    public string name;
}