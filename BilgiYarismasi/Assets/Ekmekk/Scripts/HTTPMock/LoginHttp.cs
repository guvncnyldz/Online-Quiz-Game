using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoginHttp
{
    public static void Login(string nickname, string password, Action<string,string> success, Action<string> fail)
    {
        string serverPassword = PlayerPrefs.GetString("nickname-password" + nickname, "");

        if (password != serverPassword)
        {
            fail?.Invoke("Hatalı kullanıcı adı ya da şifre!");
            return;
        }

        string email = PlayerPrefs.GetString("nickname-email" + nickname, "");
        success?.Invoke(nickname, email);
    }

    public static void Register(string nickname, string password, string email, Action<string,string> success, Action<string> fail)
    {
        string serverPassword = PlayerPrefs.GetString("nickname-password" + nickname, "");
        string serverEmail = PlayerPrefs.GetString("nickname-email" + nickname, "");

        if (serverPassword != "")
        {
            fail?.Invoke("Bu kullanıcı adı kullanılmakta");
            return;
        }

        if (serverEmail != "")
        {
            fail?.Invoke("Bu email adresi kullanılmakta");
            return;
        }

        PlayerPrefs.SetString("nickname-password" + nickname, password);
        PlayerPrefs.SetString("nickname-email" + nickname, email);

        success?.Invoke(nickname,email);
    }

    public static void AddRace(string nickname, RacesIndex racesIndex, Action success)
    {
        PlayerPrefs.SetInt("nickname-race" + nickname, (int) racesIndex);
        success?.Invoke();
    }

    public static void GetRace(string nickname, Action<RacesIndex> success, Action fail)
    {
        int raceIndex = PlayerPrefs.GetInt("nickname-race" + nickname, -1);

        if (raceIndex == -1)
        {
            fail?.Invoke();
            return;
        }

        success?.Invoke((RacesIndex) raceIndex);
    }
}