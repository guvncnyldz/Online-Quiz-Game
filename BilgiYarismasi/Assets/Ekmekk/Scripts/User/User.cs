using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class User
{
    private static User instance;

    private int race;
    private int coin;
    private int energy;
    private int money;
    private string username;
    private string email;

    public string UserId { get; private set; }
    public string ProfileId { get; private set; }

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

    public void SetUser(JArray info)
    {
        UserId = info[0]["user"]["_id"].ToString();
        ProfileId = info[0]["user"]["profile"]["_id"].ToString();
        username = info[0]["user"]["user_name"].ToString();
        email = info[0]["user"]["e_mail"].ToString();
        coin = Convert.ToInt16(info[0]["user"]["profile"]["coin"].ToString());
        race = Convert.ToInt16(info[0]["user"]["profile"]["race"].ToString());
        energy = Convert.ToInt16(info[0]["user"]["profile"]["money"].ToString());
        money = Convert.ToInt16(info[0]["user"]["profile"]["energy"].ToString());

        Token = info[0]["token"].ToString();
    }

    private async void UpdateProfile()
    {
        Debug.Log("Profile Güncellendi");
    }

    private async void AddRace()
    {
        Debug.Log("Race Güncellendi");

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

    public static User GetInstance()
    {
        if (instance == null)
        {
            instance = new User();
        }

        return instance;
    }

    public void ResetUser()
    {
        instance = null;
    }
}