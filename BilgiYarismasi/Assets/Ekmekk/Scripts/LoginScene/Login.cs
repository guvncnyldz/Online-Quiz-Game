using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField input_nickname, input_password;
    [SerializeField] private Button btn_login;
    [SerializeField] private ErrorText errorText;
    
    private LoginScene loginScene;
    private LoginScenePanelAnimation animations;

    private void Awake()
    {
        btn_login.onClick.AddListener(BeginLogin);

        loginScene = FindObjectOfType<LoginScene>();
        animations = GetComponent<LoginScenePanelAnimation>();
    }

    void BeginLogin()
    {
        string nickname = input_nickname.text;
        string password = input_password.text;
        
        if (FormInputControl.NicknameControl(nickname, WrongInput) &&
            FormInputControl.PasswordControl(password, WrongInput))
        {
            btn_login.enabled = false;
            animations.Fall(EndLogin);
            loginScene.LockPanel(false);
        }
    }

    //Sunucu bağlanınca değişecek
    void EndLogin()
    {
        string nickname = input_nickname.text;
        string password = input_password.text;

        LoginHttp.Login(nickname, password, SuccessLogin, FailLogin);
    }

    void SuccessLogin()
    {
        Debug.Log("Giriş Başarılı");
    }

    void FailLogin(string error)
    {
        animations.GoBackToPlace();
        loginScene.LockPanel(true);
        btn_login.enabled = true;
        errorText.SetError(error);
    }

    void WrongInput(string error)
    {
        animations.Shake();
        errorText.SetError(error);
    }
}