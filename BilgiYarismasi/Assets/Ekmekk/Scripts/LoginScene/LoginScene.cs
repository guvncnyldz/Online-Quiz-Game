using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour
{
    [SerializeField] private Button btn_loginPanel, btn_registerPanel;
    [SerializeField] private GameObject panel_login, panel_register;
    void Awake()
    {
        btn_loginPanel.onClick.AddListener(OpenLoginPanel);
        btn_registerPanel.onClick.AddListener(OpenRegisterPanel);
    }

    public void LockPanel(bool isUnlocked)
    {
        btn_loginPanel.enabled = isUnlocked;
        btn_registerPanel.enabled = isUnlocked;
    }
    void OpenLoginPanel()
    {
        panel_login.SetActive(true);
        panel_register.SetActive(false);
    }

    void OpenRegisterPanel()
    {
        panel_login.SetActive(false);
        panel_register.SetActive(true);
    }
}