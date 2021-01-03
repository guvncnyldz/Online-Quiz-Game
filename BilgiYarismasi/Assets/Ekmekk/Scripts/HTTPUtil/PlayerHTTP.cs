using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerHTTP
{
    public async static void GetPlayerInfo(string pid,PlayerHolder playerHolder)
    {
        var values = new Dictionary<string, string>
        {
            {"pid", pid},
        };

        JArray response = await HTTPApiUtil.Post(values, "users", "information");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
            return;
        }

        playerHolder.SetPlayer(response);
    }
}