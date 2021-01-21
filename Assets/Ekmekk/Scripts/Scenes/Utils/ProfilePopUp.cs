using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePopUp : MonoBehaviour
{
    [SerializeField] private float APPEARY = -362.4f;
    [SerializeField] private float DISAPPEARY = 413f;

    private RectTransform rectTransform;

    [SerializeField] private Image blackScreen;
    [SerializeField] private TMP_InputField tmpInputField;
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(() => { Disappear(); });

        blackScreen.enabled = false;
        rectTransform = GetComponent<RectTransform>();
    }

    public ProfilePopUp SetAndShow()
    {
        tmpInputField.placeholder.GetComponent<TextMeshProUGUI>().text = User.GetInstance().Username;
        Appear();

        return this;
    }

    public ProfilePopUp AddListenerToButton(Action action)
    {
        button.onClick.AddListener(() =>
        {
            //TODO username iap eklenecek
            User.GetInstance().Username = tmpInputField.text;
            action?.Invoke();
        });

        return this;
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