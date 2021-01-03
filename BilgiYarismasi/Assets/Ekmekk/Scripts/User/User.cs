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

    public override void SetUser(JArray info)
    {
        base.SetUser(info);

        UserId = info[0]["user"]["_id"].ToString();
        Token = info[0]["token"].ToString();
    }

    private async void UpdateProfile()
    {
        Debug.Log("Profile Güncellendi");
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