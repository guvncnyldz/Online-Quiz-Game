using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MarketItemButton : MonoBehaviour
{
    public bool isBought;
    public int gold, money;
    public string name;
    public string id;
    public CosmeticTypes type;

    public void Set(Item info, CosmeticTypes type)
    {
        this.type = type;
        name = info.name;
        gold = info.goldCost;
        money = info.moneyCost;
        id = info.id;
        GetComponent<Image>().sprite = info.sprite; 

        isBought = User.GetInstance().inventorySystem.CheckExist(id);
    }

    public async void Use(Action onBuy)
    {
        if (!isBought)
        {
            isBought = true;

            var values = new Dictionary<string, string>
            {
                {"user_id", User.GetInstance().UserId},
                {"sprite_id", id},
                {"type", type.ToString()},
                {"name", name},
            };

            JArray response = await HTTPApiUtil.Post(values, "inventory", "addinventory");

            Error error = ErrorHandler.Handle(response);

            if (error.isError)
            {
                SceneManager.LoadScene((int) Scenes.Fail);
            }

            User.GetInstance().Coin -= gold;
            User.GetInstance().Money -= money;
            User.GetInstance().inventorySystem.AddCosmetic(name,id, type);
            onBuy?.Invoke();
        }
        else
        {
            switch (type)
            {
                case CosmeticTypes.Body:
                {
                    if (User.GetInstance().cosmeticData.body != id)
                    {
                        User.GetInstance().cosmeticData.body = id;
                    }

                    break;
                }
                case CosmeticTypes.Hand:
                    if (User.GetInstance().cosmeticData.handLeft != id)
                    {
                        User.GetInstance().cosmeticData.handLeft =
                            User.GetInstance().cosmeticData.handRight = id;
                    }

                    break;
                case CosmeticTypes.Hair:
                    if (User.GetInstance().cosmeticData.hair != id)
                    {
                        User.GetInstance().cosmeticData.hair = id;
                    }

                    break;
                case CosmeticTypes.Foot:
                    if (User.GetInstance().cosmeticData.footLeft != id)
                    {
                        User.GetInstance().cosmeticData.footLeft =
                            User.GetInstance().cosmeticData.footRight = id;
                    }

                    break;
                case CosmeticTypes.Eye:
                    if (User.GetInstance().cosmeticData.eye != id)
                    {
                        User.GetInstance().cosmeticData.eye = id;
                    }

                    break;
            }

            User.GetInstance().cosmeticData.SaveCosmetics();
        }
    }
}