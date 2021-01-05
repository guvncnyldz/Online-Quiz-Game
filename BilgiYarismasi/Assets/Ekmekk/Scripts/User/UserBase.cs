using System;
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

    public virtual void SetUser(JArray info)
    {
        ProfileId = info[0]["user"]["profile"]["_id"].ToString();
        username = info[0]["user"]["profile"]["user_name"].ToString();
        race = Convert.ToInt16(info[0]["user"]["profile"]["race"].ToString());

        cosmeticData = new CosmeticData();
        cosmeticData.SetCosmetic(info);
    }
}