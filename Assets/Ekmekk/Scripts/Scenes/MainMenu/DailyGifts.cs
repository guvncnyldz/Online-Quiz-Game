using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DailyGifts : MonoBehaviour
{
    private float disappearY;
    private RectTransform rectTransform;

    [SerializeField] private Gift[] gifts;
    [SerializeField] private Transform indicator;
    [SerializeField] private TextMeshProUGUI txt_money, txt_gold;
    [SerializeField] private Image blackScreen;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        disappearY = rectTransform.anchoredPosition.y;
        blackScreen.enabled = false;
    }

    void Start()
    {
        CheckGift();
    }
    async void CheckGift()
    {
        var values = new Dictionary<string, string>
        {
            {"user_id", User.GetInstance().UserId},
        };

        JArray response = await HTTPApiUtil.Post(values, "users", "mainscreen");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
            return;
        }

        if (response[0]["code"] == null)
        {
            int lastIndex = Convert.ToInt16(response[0]["last_gift_series"].ToString()) - 1;
            int targetIndex = Convert.ToInt16(response[0]["gift_series"].ToString()) - 1;

            User.GetInstance().Coin += gifts[targetIndex].gold;
            User.GetInstance().Money += gifts[targetIndex].money;


            SetStartPosIndicator(lastIndex);

            Appear(() => { MoveIndicator(targetIndex, () => { SetMoney(targetIndex, () => { Disappear(null); }); }); });
        }
    }

    void Disappear(Action onComplete)
    {
        rectTransform.DOAnchorPosY(disappearY, 0.5f).SetEase(Ease.Linear).SetDelay(1).OnComplete(() =>
        {
            blackScreen.enabled = false;
            onComplete?.Invoke();
        });
    }

    void Appear(Action onComplete)
    {
        rectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
        blackScreen.enabled = true;
    }

    void MoveIndicator(int targetPosIndex, Action onComplete)
    {
        indicator.DOMoveX(gifts[targetPosIndex].transform.position.x, 1).SetEase(Ease.InOutCubic)
            .OnComplete(() => onComplete?.Invoke());
    }

    void SetStartPosIndicator(int targetIndex)
    {
        if (targetIndex < 0)
        {
            targetIndex = 0;
        }

        indicator.GetComponent<RectTransform>().position = gifts[targetIndex].GetComponent<RectTransform>().position;
    }

    void SetMoney(int targetIndex, Action onComplete)
    {
        int money = 0;
        int gold = 0;

        DOTween.To(() => money, x => money = x, gifts[targetIndex].money, 2)
            .OnUpdate(() => { txt_money.text = money.ToString(); })
            .OnComplete(() => { txt_money.text = money.ToString(); });

        DOTween.To(() => gold, x => gold = x, gifts[targetIndex].gold, 2).OnUpdate(() =>
        {
            txt_gold.text = gold.ToString();
        }).OnComplete(() =>
        {
            txt_gold.text = gold.ToString();
            onComplete?.Invoke();
        });
    }
}