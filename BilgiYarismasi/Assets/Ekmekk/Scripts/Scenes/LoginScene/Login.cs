using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

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
            animations.Fall();
            loginScene.LockPanel(false);
            LoginHttp.Login(nickname, password, SuccessLogin, FailLogin);
        }
    }

    void SuccessLogin()
    {
        if (User.GetInstance().Race == -1)
        {
            SceneManager.LoadScene((int) Scenes.Race);
        }
        else
        {
            SceneManager.LoadScene((int) Scenes.Main);
        }
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