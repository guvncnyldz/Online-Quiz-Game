using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketPopUp : MonoBehaviour
{
    [SerializeField] private float APPEARY = -362.4f;
    [SerializeField] private float DISAPPEARY = 413f;

    private RectTransform rectTransform;

    [SerializeField] private TextMeshProUGUI money, gold;
    [SerializeField] private Button btn_buy, btn_no;
    [SerializeField] private Image blackScreen;

    private void Awake()
    {
        btn_no.onClick.AddListener(() => { Disappear(); });
        blackScreen.enabled = false;

        rectTransform = GetComponent<RectTransform>();
    }

    public MarketPopUp SetAndShow(int money, int gold)
    {
        Set(money, gold);
        Appear();

        return this;
    }

    public MarketPopUp AddListenerToButton(Action action)
    {
        btn_buy.onClick.AddListener(() => { action?.Invoke(); });

        return this;
    }

    public MarketPopUp ResetListenerFromButton(bool isDefault = true)
    {
        btn_buy.onClick.RemoveAllListeners();

        if (isDefault)
            btn_buy.onClick.AddListener(() => { Disappear(); });

        return this;
    }

    public MarketPopUp Set(int money, int gold)
    {
        this.money.text = money.ToString();
        this.gold.text = gold.ToString();

        return this;
    }

    public MarketPopUp Appear(float duration = 0.5f, Action onComplete = null)
    {
        rectTransform.DOAnchorPos(new Vector2(0, APPEARY), 0.5f).OnComplete(() =>
            onComplete?.Invoke()).SetEase(Ease.Linear);
        blackScreen.enabled = true;


        return this;
    }

    public MarketPopUp Disappear(float duration = 0.5f, Action onComplete = null)
    {
        rectTransform.DOAnchorPos(new Vector2(0, DISAPPEARY), 0.5f).OnComplete(() =>
        {
            blackScreen.enabled = false;
            onComplete?.Invoke();
        }).SetEase(Ease.Linear);


        return this;
    }
}