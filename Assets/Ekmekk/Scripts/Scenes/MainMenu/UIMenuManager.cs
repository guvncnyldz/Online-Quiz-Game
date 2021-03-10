using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private RaceMenuSprites[] raceMenuSpriteses;
    [SerializeField] private Image background, bottomMenu, settingsButton, shopButton, playButton;
    [SerializeField] private RectTransform topRect, bottomRect, gameTypeRect, leftRect, rightRect;
    [SerializeField] private TextMeshProUGUI txt_gold, txt_money;
    private Button[] buttons;

    public void Awake()
    {
        SetMoney();
        
        buttons = FindObjectsOfType<Button>();

        background.sprite = raceMenuSpriteses[User.GetInstance().Race].background;
        bottomMenu.sprite = raceMenuSpriteses[User.GetInstance().Race].bottomMenu;
        settingsButton.sprite = raceMenuSpriteses[User.GetInstance().Race].bottomButton;
        shopButton.sprite = raceMenuSpriteses[User.GetInstance().Race].bottomButton;
        playButton.sprite = raceMenuSpriteses[User.GetInstance().Race].playButton;

        MoveTop();
        MoveBottom();
        MoveLeft();
        MoveRight();
    }

    private void Start()
    {
        FindObjectOfType<DailyGifts>().CheckGift(() =>
        {
            FindObjectOfType<TournamentResult>().CheckResult();
        });
    }

    public void SetMoney()
    {
        float currentGold = Convert.ToInt64(txt_gold.text);
        float currentMoney = Convert.ToInt64(txt_money.text);

        DOTween.To(() => currentGold, x => currentGold = Mathf.Floor(x), User.GetInstance().Coin, 1).OnUpdate(() =>
        {
            txt_gold.text = currentGold.ToString();
        }).OnComplete(() =>
        {
            txt_gold.text = User.GetInstance().Coin.ToString();
        });;
        
        DOTween.To(() => currentMoney, x => currentMoney = Mathf.Floor(x), User.GetInstance().Money, 1).OnUpdate(() =>
        {
            txt_money.text = currentMoney.ToString();
        }).OnComplete(() =>
        {
            txt_money.text = User.GetInstance().Money.ToString();
        });
    }
    public void MoveTop()
    {
        float pos = topRect.anchoredPosition.y * -1;

        topRect.DOAnchorPosY(pos, 0.5f).SetEase(Ease.Linear);
    }

    public void MoveBottom()
    {
        float pos = bottomRect.anchoredPosition.y * -1;

        LockButton(true);

        bottomRect.DOAnchorPosY(pos, 0.5f).SetEase(Ease.Linear).OnComplete(() => LockButton(false));
    }

    public void MoveLeft()
    {
        float pos = leftRect.anchoredPosition.x * -1;

        leftRect.DOAnchorPosX(pos, 0.5f).SetEase(Ease.Linear);
    }

    public void MoveRight()
    {
        float pos = rightRect.anchoredPosition.x * -1;

        rightRect.DOAnchorPosX(pos, 0.5f).SetEase(Ease.Linear);
    }
    public void MoveGameType()
    {
        float pos = gameTypeRect.anchoredPosition.y * -1;

        gameTypeRect.DOAnchorPosY(pos, 0.5f).SetEase(Ease.Linear);
    }

    public void LockButton(bool isLocked)
    {
        foreach (Button button in buttons)
        {
            button.enabled = !isLocked;
        }
    }
}

[Serializable]
public class RaceMenuSprites
{
    public Sprite background, bottomMenu, bottomButton, playButton;
}