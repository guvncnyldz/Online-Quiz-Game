using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class LoginHttp
{
    public async static void Login(string nickname, string password, Action success,
        Action<string> fail)
    {
        var values = new Dictionary<string, string>
        {
            {"user_name", nickname},
            {"password", password},
            {"device_id", SystemInfo.deviceUniqueIdentifier}
        };

        JArray response = await HTTPAuthUtil.Post(values, "auth", "login");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            fail.Invoke(error.message);
            return;
        }

        User.GetInstance().SetUser(response);
        success.Invoke();
    }

    public async static void Register(string nickname, string password, string email, Action success,
        Action<string> fail)
    {
        var values = new Dictionary<string, string>
        {
            {"user_name", nickname},
            {"password", password},
            {"e_mail", email},
            {"device_id", SystemInfo.deviceUniqueIdentifier}
        };

        JArray response = await HTTPAuthUtil.Post(values, "auth", "register");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            fail.Invoke(error.message);
            return;
        }

        User.GetInstance().SetUser(response);
        success.Invoke();
    }

    public static void Logout()
    {
        var values = new Dictionary<string, string>
        {
            {"device_id", SystemInfo.deviceUniqueIdentifier}
        };

        HTTPAuthUtil.Post(values, "auth", "logout");
    }
}