using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private TMP_InputField input_nickname, input_password;
    [SerializeField] private Button btn_login;
    [SerializeField] private ErrorText errorText;

    private LoginScene loginScene;
    private LoginScenePanelAnimation animations;

    private RectTransform rectTransform;

    private void Awake()
    {
        btn_login.onClick.AddListener(BeginLogin);

        input_nickname.onSelect.AddListener((s) => { MoveForKeyboard(true); });
        input_nickname.onDeselect.AddListener((s) => { MoveForKeyboard(false); });
        input_password.onSelect.AddListener((s) => { MoveForKeyboard(true); });
        input_password.onDeselect.AddListener((s) => { MoveForKeyboard(false); });

        rectTransform = GetComponent<RectTransform>();
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

    void SuccessLogin(string nickname, string email)
    {
        User.GetInstance.email = email;
        User.GetInstance.nickname = nickname;

        LoginHttp.GetRace(nickname, (race) =>
            {
                User.GetInstance.race = race;
                SceneManager.LoadScene((int) Scenes.Main);
            },
            () => { SceneManager.LoadScene((int) Scenes.Race); });
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

    public void MoveForKeyboard(bool isKeyboardOpen)
    {
        if (isKeyboardOpen)
        {
            rectTransform.DOAnchorPos(new Vector2(0, 183), 0.5f);
        }
        else
        {
            rectTransform.DOAnchorPos(new Vector2(0, -17), 0.5f);
        }
    }
}