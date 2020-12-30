using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    private void Awake()
    {
        DeviceControl();
    }

    async void DeviceControl()
    {
        Debug.Log(Application.version);

        var values = new Dictionary<string, string>
        {
            {"version", Application.version},
            {"device_id", SystemInfo.deviceUniqueIdentifier}
        };

        JArray response = await HTTPAuthUtil.Post(values, "auth", "devicecontrol");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            if (error.errorCode == ErrorHandler.HTTPVersionNotSupported)
            {
                Debug.Log(error.errorCode + " " + error.message);
            }
            else
            {
                SceneManager.LoadScene((int) Scenes.Login);
            }

            return;
        }

        User.GetInstance().SetUser(response);

        if (User.GetInstance().Race == -1)
        {
            SceneManager.LoadScene((int) Scenes.Race);
        }
        else
        {
            SceneManager.LoadScene((int) Scenes.Main);
        }
    }
}