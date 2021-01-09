using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MarketItemButton : MonoBehaviour
{
    public bool isBought;
    public int gold, money;
    public string name;
    public string sprite_name;
    public CosmeticTypes type;

    public void Set(JToken info, CosmeticTypes type)
    {
        this.type = type;
        name = info["item_name"].ToString();
        gold = Convert.ToInt16(info["gold_cost"].ToString());
        money = Convert.ToInt16(info["money_cost"].ToString());
        sprite_name = info["sprite_name"].ToString();
        GetComponent<Image>().sprite =
            Resources.Load<Sprite>("Cosmetics/Character/" + type + "/" + sprite_name);

        isBought = User.GetInstance().inventorySystem.CheckExist(sprite_name);
    }

    public async void Use(Action onBuy)
    {
        if (!isBought)
        {
            isBought = true;

            var values = new Dictionary<string, string>
            {
                {"user_id", User.GetInstance().UserId},
                {"sprite_name", sprite_name},
                {"type", type.ToString()},
            };

            JArray response = await HTTPApiUtil.Post(values, "inventory", "addinventory");

            Error error = ErrorHandler.Handle(response);

            if (error.isError)
            {
                SceneManager.LoadScene((int) Scenes.Fail);
            }

            User.GetInstance().Coin -= gold;
            User.GetInstance().Money -= money;
            User.GetInstance().inventorySystem.AddCosmetic(sprite_name, type);
            onBuy?.Invoke();
        }
        else
        {
            switch (type)
            {
                case CosmeticTypes.Body:
                {
                    if (User.GetInstance().cosmeticData.body != sprite_name)
                    {
                        User.GetInstance().cosmeticData.body = sprite_name;
                    }

                    break;
                }
                case CosmeticTypes.Hand:
                    if (User.GetInstance().cosmeticData.handLeft != sprite_name)
                    {
                        User.GetInstance().cosmeticData.handLeft =
                            User.GetInstance().cosmeticData.handRight = sprite_name;
                    }

                    break;
                case CosmeticTypes.Hair:
                    if (User.GetInstance().cosmeticData.hair != sprite_name)
                    {
                        User.GetInstance().cosmeticData.hair = sprite_name;
                    }

                    break;
                case CosmeticTypes.Foot:
                    if (User.GetInstance().cosmeticData.footLeft != sprite_name)
                    {
                        User.GetInstance().cosmeticData.footLeft =
                            User.GetInstance().cosmeticData.footRight = sprite_name;
                    }

                    break;
                case CosmeticTypes.Eye:
                    if (User.GetInstance().cosmeticData.eye != sprite_name)
                    {
                        User.GetInstance().cosmeticData.eye = sprite_name;
                    }

                    break;
            }

            User.GetInstance().cosmeticData.SaveCosmetics();
        }
    }
}