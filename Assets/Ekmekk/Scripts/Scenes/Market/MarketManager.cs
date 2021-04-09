using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    [SerializeField] private RectTransform bottomMenu, rightMenu, jokerMenu, moneyMenu;
    [SerializeField] private Button btn_hair, btn_eye, btn_body, btn_foot, btn_hand, btn_joker, btn_money;
    [SerializeField] private TextMeshProUGUI txt_money, txt_gold;
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Image background;
    private CosmeticTypes currentType;
    private MarketScrollPanel marketScrollPanel;
    private MarketJokerScrollPanel jokerScrollPanel;
    private MarketMoneyScrollPanel moneyScrollPanel;

    private bool isBottomMenuOpen, isJokerMenuOpen, isMoneyMenuOpen;

    private void SetMoney()
    {
        txt_money.text = User.GetInstance().Money.ToString();
        txt_gold.text = User.GetInstance().Coin.ToString();
    }

    private void Awake()
    {
        background.sprite = backgrounds[User.GetInstance().Race];
        SetMoney();

        btn_money.onClick.AddListener(() =>
        {
            if (isMoneyMenuOpen)
                return;

            LockButton(true);

            if (isBottomMenuOpen)
            {
                MoveBottomMenu(() => { MoveMoneyMenu(() => LockButton(false)); });
                isBottomMenuOpen = false;
                isMoneyMenuOpen = true;
                return;
            }

            if (isJokerMenuOpen)
            {
                MoveJokerMenu(() => { MoveMoneyMenu(() => LockButton(false)); });
                isJokerMenuOpen = false;
                isMoneyMenuOpen = true;
                return;
            }
        });

        btn_joker.onClick.AddListener(() =>
        {
            if (isJokerMenuOpen)
                return;

            LockButton(true);

            if (isBottomMenuOpen)
            {
                MoveBottomMenu(() => { MoveJokerMenu(() => LockButton(false)); });
                isBottomMenuOpen = false;
                isJokerMenuOpen = true;
                return;
            }

            if (isMoneyMenuOpen)
            {
                MoveMoneyMenu(() => { MoveJokerMenu(() => LockButton(false)); });
                isMoneyMenuOpen = false;
                isJokerMenuOpen = true;
                return;
            }
        });

        btn_body.onClick.AddListener(() => { CosmeticButton(CosmeticTypes.Body); });
        btn_hair.onClick.AddListener(() => { CosmeticButton(CosmeticTypes.Hair); });
        btn_eye.onClick.AddListener(() => { CosmeticButton(CosmeticTypes.Eye); });
        btn_foot.onClick.AddListener(() => { CosmeticButton(CosmeticTypes.Foot); });
        btn_hand.onClick.AddListener(() => { CosmeticButton(CosmeticTypes.Hand); });

        marketScrollPanel = FindObjectOfType<MarketScrollPanel>();
        jokerScrollPanel = FindObjectOfType<MarketJokerScrollPanel>();
        moneyScrollPanel = FindObjectOfType<MarketMoneyScrollPanel>();
        marketScrollPanel.onBuy = SetMoney;
        jokerScrollPanel.onBuy = SetMoney;
        moneyScrollPanel.onBuy = SetMoney;
    }

    private void MoveMoneyMenu(Action onComplete)
    {
        float pos = moneyMenu.anchoredPosition.y * -1;

        moneyMenu.DOAnchorPosY(pos, 0.25f).OnComplete(() => { onComplete?.Invoke(); }).SetEase(Ease.Linear);
    }

    void CosmeticButton(CosmeticTypes type)
    {
        if (currentType == type && isBottomMenuOpen)
            return;

        LockButton(true);

        if (isJokerMenuOpen)
        {
            MoveJokerMenu(null);
            isJokerMenuOpen = false;
            currentType = type;
            marketScrollPanel.SetMarketItems(currentType);
            MoveBottomMenu(() => LockButton(false));
            isBottomMenuOpen = true;
            return;
        }

        if (isMoneyMenuOpen)
        {
            MoveMoneyMenu(null);
            isMoneyMenuOpen = false;
            currentType = type;
            marketScrollPanel.SetMarketItems(currentType);
            MoveBottomMenu(() => LockButton(false));
            isBottomMenuOpen = true;
            return;
        }

        isBottomMenuOpen = true;

        MoveBottomMenu(() =>
        {
            currentType = type;
            marketScrollPanel.SetMarketItems(currentType);
            MoveBottomMenu(() => LockButton(false));
        });
    }

    private void Start()
    {
        GetMarketItems();
    }
    void GetMarketItems()
    {
        marketScrollPanel.SetMarketItems(CosmeticTypes.Body);
        currentType = CosmeticTypes.Body;
        MoveBottomMenu(null);
        isBottomMenuOpen = true;
        MoveRightMenu();
    }

    void MoveBottomMenu(Action onComplete)
    {
        float pos = bottomMenu.anchoredPosition.y * -1;

        bottomMenu.DOAnchorPosY(pos, 0.25f).OnComplete(() => { onComplete?.Invoke(); }).SetEase(Ease.Linear);
    }

    void MoveJokerMenu(Action onComplete)
    {
        float pos = jokerMenu.anchoredPosition.y * -1;

        jokerMenu.DOAnchorPosY(pos, 0.25f).OnComplete(() => { onComplete?.Invoke(); }).SetEase(Ease.Linear);
    }

    void MoveRightMenu()
    {
        float pos = rightMenu.anchoredPosition.x * -1;

        rightMenu.DOAnchorPosX(pos, 0.25f).SetEase(Ease.Linear);
    }

    void LockButton(bool isLocked)
    {
        btn_hair.enabled = !isLocked;
        btn_eye.enabled = !isLocked;
        btn_body.enabled = !isLocked;
        btn_foot.enabled = !isLocked;
        btn_hand.enabled = !isLocked;
        btn_joker.enabled = !isLocked;
        btn_money.enabled = !isLocked;
    }
}