using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public void Logout()
    {
        LoginHttp.Logout();
        SceneManager.LoadScene((int) Scenes.Login);
    }

    public void TrainingMode()
    {
        SceneManager.LoadScene((int) Scenes.TrainingGame);
    }
}