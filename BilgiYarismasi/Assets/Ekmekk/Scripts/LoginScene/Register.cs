using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    [SerializeField] private TMP_InputField input_nickname, input_password, input_passwordAgain, input_email;
    [SerializeField] private Button btn_register;
    [SerializeField] private ErrorText errorText;

    private LoginScene loginScene;
    private LoginScenePanelAnimation animations;

    private RectTransform rectTransform;

    private void Awake()
    {
        btn_register.onClick.AddListener(BeginRegister);

        input_nickname.onSelect.AddListener((s) => { MoveForKeyboard(true); });
        input_nickname.onDeselect.AddListener((s) => { MoveForKeyboard(false); });
        input_password.onSelect.AddListener((s) => { MoveForKeyboard(true); });
        input_password.onDeselect.AddListener((s) => { MoveForKeyboard(false); });
        input_passwordAgain.onSelect.AddListener((s) => { MoveForKeyboard(true); });
        input_passwordAgain.onDeselect.AddListener((s) => { MoveForKeyboard(false); });
        input_email.onSelect.AddListener((s) => { MoveForKeyboard(true); });
        input_email.onDeselect.AddListener((s) => { MoveForKeyboard(false); });
        
        rectTransform = GetComponent<RectTransform>();
        loginScene = FindObjectOfType<LoginScene>();
        animations = GetComponent<LoginScenePanelAnimation>();
    }

    void BeginRegister()
    {
        string nickname = input_nickname.text;
        string password = input_password.text;
        string passwordAgain = input_passwordAgain.text;
        string email = input_email.text;

        if (password != passwordAgain)
        {
            WrongInput("Şifreler uyuşmuyor!");
            return;
        }

        if (FormInputControl.NicknameControl(nickname, WrongInput) &&
            FormInputControl.PasswordControl(password, WrongInput) &&
            FormInputControl.EmailControl(email, WrongInput))
        {
            animations.Fall(EndRegister);
            loginScene.LockPanel(true);
            btn_register.enabled = false;
        }
    }

    //Sunucu bağlanınca değişecek
    void EndRegister()
    {
        string nickname = input_nickname.text;
        string password = input_password.text;
        string email = input_email.text;

        LoginHttp.Register(nickname, password, email, SuccessRegister, FailRegister);
    }

    void SuccessRegister(string nickname, string email)
    {
        User.GetInstance.nickname = nickname;
        User.GetInstance.email = email;

        SceneManager.LoadScene((int) Scenes.Race);
    }

    void FailRegister(string error)
    {
        animations.GoBackToPlace();
        loginScene.LockPanel(true);
        btn_register.enabled = true;
        errorText.SetError(error);
    }

    void WrongInput(string error)
    {
        animations.Shake();
        errorText.SetError(error);
    }

    public void MoveForKeyboard(bool isKeyboardOpen)
    {
        if (isKeyboardOpen)
        {
            rectTransform.DOAnchorPos(new Vector2(0, 171), 0.5f);
        }
        else
        {
            rectTransform.DOAnchorPos(new Vector2(0, -41), 0.5f);
        }
    }
}