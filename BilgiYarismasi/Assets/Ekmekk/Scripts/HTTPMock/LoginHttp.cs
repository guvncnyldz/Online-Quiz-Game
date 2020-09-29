using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoginHttp
{
    public static void Login(string nickname, string password, Action success, Action<string> fail)
    {
        string serverPassword = PlayerPrefs.GetString("nickname-password" + nickname, "");

        if (password != serverPassword)
        {
            fail?.Invoke("Hatalı kullanıcı adı ya da şifre!");
            return;
        }
        
        success?.Invoke();
    }

    public static void Register(string nickname, string password, string email, Action success, Action<string> fail)
    {
        string serverPassword = PlayerPrefs.GetString("nickname-password" + nickname, "");
        string serverNickname = PlayerPrefs.GetString("email-nickname" + email, "");
        
        if (serverPassword != "")
        {
            fail?.Invoke("Bu kullanıcı adı kullanılmakta");
            return;
        }

        if (serverNickname != "")
        {
            fail?.Invoke("Bu email adresi kullanılmakta");
            return;
        }
        
        PlayerPrefs.SetString("nickname-password" + nickname,password);
        PlayerPrefs.SetString("email-nickname" + email,nickname);
        
        success?.Invoke();
    }
}