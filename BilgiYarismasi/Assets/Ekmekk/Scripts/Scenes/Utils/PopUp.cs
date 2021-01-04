using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    private const float APPEARY = -362.4f;
    private const float DISAPPEARY = 413f;

    private RectTransform rectTransform;

    [SerializeField] private TextMeshProUGUI message, header;
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(() => { Disappear(); });

        rectTransform = GetComponent<RectTransform>();
    }

    public PopUp SetAndShow(string header, string message)
    {
        Set(header, message);
        Appear();
        
        return this;
    }

    public PopUp AddListenerToButton(Action action)
    {
        button.onClick.AddListener(() => { action?.Invoke(); });
        
        return this;

    }

    public PopUp ResetListenerFromButton()
    {
        button.onClick.RemoveAllListeners();
        
        return this;

    }

    public PopUp Set(string header, string message)
    {
        this.header.text = header;
        this.message.text = message;
        
        return this;

    }

    public PopUp Appear(float duration = 0.5f, Action onComplete = null)
    {
        rectTransform.DOAnchorPos(new Vector2(0, APPEARY), 0.5f).OnComplete(() =>
            onComplete?.Invoke()).SetEase(Ease.Linear);
        
        return this;

    }

    public PopUp Disappear(float duration = 0.5f, Action onComplete = null)
    {
        rectTransform.DOAnchorPos(new Vector2(0, DISAPPEARY), 0.5f).OnComplete(() =>
            onComplete?.Invoke()).SetEase(Ease.Linear);

        return this;
    }
}