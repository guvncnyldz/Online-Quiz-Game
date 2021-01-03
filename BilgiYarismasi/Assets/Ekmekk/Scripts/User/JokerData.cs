using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JokerData
{
    private int pass = 10, bomb = 10, correct = 10;

    public int Pass
    {
        get => pass;
        set
        {
            pass = value;
            UpdateJoker();
        }
    }

    public int Bomb
    {
        get => bomb;
        set
        {
            bomb = value;
            UpdateJoker();
        }
    }

    public int Correct
    {
        get => correct;
        set
        {
            correct = value;
            UpdateJoker();
        }
    }

    public void SetJoker(JArray info)
    {
        correct = Convert.ToInt16(info[0]["user"]["profile"]["joker"]["correct"].ToString());
        pass = Convert.ToInt16(info[0]["user"]["profile"]["joker"]["pass"].ToString());
        bomb = Convert.ToInt16(info[0]["user"]["profile"]["joker"]["bomb"].ToString());
    }

    public async void UpdateJoker()
    {
        var values = new Dictionary<string, string>
        {
            //User id ile daha güvenli olur. Şimdilik durabilir
            {"pid", User.GetInstance().ProfileId},
            {"bomb", bomb.ToString()},
            {"correct", correct.ToString()},
            {"pass", pass.ToString()},
        };

        JArray response = await HTTPApiUtil.Put(values, "joker", "updatejoker");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
        }
    }
}