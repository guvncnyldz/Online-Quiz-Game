using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WordHttp : MonoBehaviour
{
    public async static void GetWord(Action success, int count = 1)
    {
        if (count <= 0)
        {
            success?.Invoke();
            return;
        }

        var values = new Dictionary<string, string>
        {
            {"count", count.ToString()},
        };

        JArray response = await HTTPApiUtil.Post(values, "word", "getWord");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
            return;
        }

        WordPool.GetInstance.AddWord(response);
        success?.Invoke();
    }
}
