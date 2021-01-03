using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ErrorUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_error,txt_errorMessage,txt_header;
    [SerializeField] private Button button;

    private void Awake()
    {
        txt_header.text = "Eyvah!";
        txt_error.text = "Bir şeyler ters gitti ve sorun tarafımıza iletildi. En kısa zamanda çözeceğiz";

        txt_errorMessage.text = PlayerPrefs.GetString("errorMessage", "");
        
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene((int) Scenes.Opening);
        });
    }
}
