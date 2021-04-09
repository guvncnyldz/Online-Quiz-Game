using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfilePopUp : MonoBehaviour
{
    [SerializeField] private float APPEARY = -362.4f;
    [SerializeField] private float DISAPPEARY = 413f;

    private RectTransform rectTransform;
    [SerializeField] private ErrorText errorText;
    [SerializeField] private Image blackScreen;
    [SerializeField] private TMP_InputField tmpInputField;
    [SerializeField] private Button button;

    private void Awake()
    {
        blackScreen.enabled = false;
        rectTransform = GetComponent<RectTransform>();
    }

    public ProfilePopUp SetAndShow()
    {
        tmpInputField.placeholder.GetComponent<TextMeshProUGUI>().text = User.GetInstance().Username;
        Appear();

        return this;
    }

    public ProfilePopUp AddListenerToButton(TextMeshProUGUI txt_name)
    {
        button.onClick.AddListener(async () =>
        {
            
            if (FormInputControl.NicknameControl(tmpInputField.text, WrongInput))
            {
                var values = new Dictionary<string, string>
                {
                    {"user_name", tmpInputField.text},
                };

                JArray response = await HTTPApiUtil.Post(values, "users", "isuserexist");

                Error error = ErrorHandler.Handle(response);

                if (error.errorCode == ErrorHandler.Forbidden)
                {
                    FindObjectOfType<IAP>().BuyConsumable("changeName", () =>
                    {
                        User.GetInstance().Username = tmpInputField.text;
                        txt_name.text = tmpInputField.text;

                        WrongInput("");
                    });
                    
                    Disappear();
                }
                else if (error.isError)
                {
                    SceneManager.LoadScene((int) Scenes.Fail);
                    return;
                }
                else
                {
                    WrongInput("Bu kullanıcı bulunmakta");
                }
            }
        });

        return this;
    }

    void WrongInput(string error)
    {
        errorText.SetError(error);
    }

    public ProfilePopUp ResetListenerFromButton(bool isDefault = true)
    {
        button.onClick.RemoveAllListeners();

        if (isDefault)
            button.onClick.AddListener(() => { Disappear(); });

        return this;
    }

    public ProfilePopUp Appear(float duration = 0.5f, Action onComplete = null)
    {
        rectTransform.DOAnchorPos(new Vector2(0, APPEARY), 0.5f).OnComplete(() =>
            onComplete?.Invoke()).SetEase(Ease.Linear);
        blackScreen.enabled = true;


        return this;
    }

    public ProfilePopUp Disappear(float duration = 0.5f, Action onComplete = null)
    {
        rectTransform.DOAnchorPos(new Vector2(0, DISAPPEARY), 0.5f).OnComplete(() =>
        {
            blackScreen.enabled = false;
            onComplete?.Invoke();
        }).SetEase(Ease.Linear);
        blackScreen.enabled = false;


        return this;
    }
}