﻿using UnityEngine;
using UnityEngine.SceneManagement;
public class ErrorUI : MonoBehaviour
{
    private void Awake()
    {
        string header = "Eyvah!";
        string message = "Bir şeyler ters gitti ve sorun tarafımıza iletildi. En kısa zamanda çözeceğiz";

        FindObjectOfType<PopUp>().AddListenerToButton(() =>
        {
            SceneManager.LoadScene((int) Scenes.Opening);
        }).SetAndShow(header,message);
    }
}
