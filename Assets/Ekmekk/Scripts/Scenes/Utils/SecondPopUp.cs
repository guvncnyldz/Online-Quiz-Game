using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SecondPopUp : MonoBehaviour
{
    [SerializeField] private float APPEARY = -362.4f;
    [SerializeField] private float DISAPPEARY = 413f;

    private RectTransform rectTransform;

    [SerializeField] private TextMeshProUGUI message, header;
    [SerializeField] private Button btn_cancel, btn_ok;
    [SerializeField] private Image blackScreen;

    private bool isOpened;

    private Tween lastTween;

    private void Awake()
    {
        btn_cancel.onClick.AddListener(() => { Disappear(); });
        blackScreen.enabled = false;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isOpened)
        {
            btn_cancel.onClick.Invoke();
        }
    }

    public SecondPopUp SetAndShow(string header, string message, string buttonText)
    {
        Set(header, message, buttonText);
        Appear();

        return this;
    }

    public SecondPopUp AddListenerToSecondButton(Action action)
    {
        btn_ok.onClick.AddListener(() => { action?.Invoke(); });

        return this;
    }

    public SecondPopUp AddListenerToButton(Action action)
    {
        btn_cancel.onClick.AddListener(() => { action?.Invoke(); });

        return this;
    }

    public SecondPopUp ResetListenerFromSecondButton(bool isDefault = true)
    {
        btn_ok.onClick.RemoveAllListeners();

        return this;
    }

    public SecondPopUp ResetListenerFromButton(bool isDefault = true)
    {
        btn_cancel.onClick.RemoveAllListeners();

        if (isDefault)
            btn_cancel.onClick.AddListener(() => { Disappear(); });

        return this;
    }

    public SecondPopUp Set(string header, string message, string buttonText)
    {
        this.header.text = header;
        this.message.text = message;
        btn_ok.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
        return this;
    }

    public SecondPopUp Appear(float duration = 0.5f, Action onComplete = null)
    {
        if (lastTween != null)
        {
            lastTween.Kill();
        }

        lastTween = rectTransform.DOAnchorPos(new Vector2(0, APPEARY), 0.5f).OnComplete(() =>
        {
            blackScreen.enabled = true;
            isOpened = true;
            onComplete?.Invoke();
        }).SetEase(Ease.Linear);
        blackScreen.enabled = true;


        return this;
    }

    public SecondPopUp Disappear(float duration = 0.5f, Action onComplete = null)
    {
        if (lastTween != null)
        {
            lastTween.Kill();
        }

        lastTween = rectTransform.DOAnchorPos(new Vector2(0, DISAPPEARY), 0.5f).OnComplete(() =>
        {
            blackScreen.enabled = false;
            onComplete?.Invoke();
        }).SetEase(Ease.Linear);
        isOpened = false;

        return this;
    }
}