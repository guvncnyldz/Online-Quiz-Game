﻿using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public abstract class UserBase
{
    public string ProfileId { get; protected set; }
    public CosmeticData cosmeticData;

    protected int race;
    protected int coin;
    protected int energy;
    protected int money;
    protected string username;
    protected string email;

    public virtual void SetUser(JToken info)
    {
        ProfileId = info["_id"].ToString();
        username = info["user_name"].ToString();
        race = Convert.ToInt16(info["race"].ToString());

        cosmeticData = new CosmeticData();
        cosmeticData.SetCosmetic(info["cosmetic"]);
    }
    
    public void SetUser(string id, string username, int race, CosmeticData cosmeticData)
    {
        ProfileId = id;
        this.username = username;
        this.race = race;

        this.cosmeticData = new CosmeticData();
        this.cosmeticData.SetCosmetic(cosmeticData);
    }
}