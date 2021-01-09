using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventorySystem : MonoBehaviour
{
    private List<Inventory> cosmetic;

    public InventorySystem()
    {
        cosmetic = new List<Inventory>();
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
            Inventory inventory = new Inventory();
            inventory.sprite_name = jToken["sprite_name"].ToString();

            switch (jToken["type"].ToString())
            {
                case "Head":
                    inventory.type = CosmeticTypes.Head;
                    break;
                case "Hair":
                    inventory.type = CosmeticTypes.Hair;
                    break;
                case "Hand":
                    inventory.type = CosmeticTypes.Hand;
                    break;
                case "Foot":
                    inventory.type = CosmeticTypes.Foot;
                    break;
                case "Body":
                    inventory.type = CosmeticTypes.Body;
                    break;
            }

            cosmetic.Add(inventory);
        }
    }

    public bool CheckExist(string sprite_name)
    {
        foreach (Inventory inventory in cosmetic)
        {
            if (inventory.sprite_name == sprite_name)
                return true;
        }

        return false;
    }

    public void AddCosmetic(string spriteName, CosmeticTypes type)
    {
        Inventory inventory = new Inventory();
        inventory.sprite_name = spriteName;
        inventory.type = type;
        
        cosmetic.Add(inventory);
    }
}

public class Inventory
{
    public string sprite_name;
    public CosmeticTypes type;
}