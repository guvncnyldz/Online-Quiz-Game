using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class User : UserBase
{
    private static User instance;

    public JokerData jokerData;
    public string UserId { get; private set; }

    public string Username
    {
        get => username;
        set
        {
            username = value;
            UpdateProfile();
        }
    }

    public string Email
    {
        get => email;
        set
        {
            email = value;
            UpdateProfile();
        }
    }

    public int Race
    {
        get => race;
        set
        {
            race = value;
            AddRace();
        }
    }

    public int Coin
    {
        get => coin;
        set
        {
            coin = value;
            UpdateProfile();
        }
    }

    public int Money
    {
        get => money;
        set
        {
            money = value;
            UpdateProfile();
        }
    }

    public int Energy
    {
        get => energy;
        set
        {
            energy = value;
            UpdateProfile();
        }
    }

    public string Token { get; private set; }

    public User()
    {
        jokerData = new JokerData();
    }
    public override void SetUser(JArray info)
    {
        base.SetUser(info);

        email = info[0]["user"]["e_mail"].ToString();
        UserId = info[0]["user"]["_id"].ToString();
        Token = info[0]["token"].ToString();
        energy = Convert.ToInt16(info[0]["user"]["profile"]["money"].ToString());
        money = Convert.ToInt16(info[0]["user"]["profile"]["energy"].ToString());
        coin = Convert.ToInt16(info[0]["user"]["profile"]["coin"].ToString());
        
        jokerData = new JokerData();
        jokerData.SetJoker(info);
    }

    private async void UpdateProfile()
    {
        var values = new Dictionary<string, string>
        {
            //User id ile daha güvenli olur. Şimdilik durabilir
            {"uid", UserId},
            {"user_name", Username},
            {"coin", Coin.ToString()},
            {"money", Money.ToString()},
            {"energy", Energy.ToString()},
            {"e_mail", Email},
        };

        JArray response = await HTTPApiUtil.Put(values, "users", "updateuser");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
        }
    }

    private async void AddRace()
    {
        var values = new Dictionary<string, string>
        {
            {"pid", ProfileId},
            {"race", race.ToString()},
        };

        JArray response = await HTTPApiUtil.Post(values, "users", "addRace");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
        }
    }

    public void ResetUser()
    {
        instance = null;
    }

    public static User GetInstance()
    {
        if (instance == null)
        {
            instance = new User();
        }

        return instance;
    }
}