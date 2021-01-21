using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private RectTransform bottomMenu, rightMenu, jokerMenu;
    [SerializeField] private Button btn_head, btn_hair, btn_eye, btn_body, btn_foot, btn_hand, btn_joker;
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Image background;
    private CosmeticTypes currentType;
    private InventoryScrollPanel inventoryScrollPanel;

    private bool isBottomMenuOpen, isJokerMenuOpen;

    private void Awake()
    {
        background.sprite = backgrounds[User.GetInstance().Race];

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
        });

        btn_body.onClick.AddListener(() => { CosmeticButton(CosmeticTypes.Body); });
        btn_head.onClick.AddListener(() => { CosmeticButton(CosmeticTypes.Head); });
        btn_hair.onClick.AddListener(() => { CosmeticButton(CosmeticTypes.Hair); });
        btn_eye.onClick.AddListener(() => { CosmeticButton(CosmeticTypes.Eye); });
        btn_foot.onClick.AddListener(() => { CosmeticButton(CosmeticTypes.Foot); });
        btn_hand.onClick.AddListener(() => { CosmeticButton(CosmeticTypes.Hand); });

        inventoryScrollPanel = FindObjectOfType<InventoryScrollPanel>();
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
            inventoryScrollPanel.SetItems(currentType);
            MoveBottomMenu(() => LockButton(false));
            isBottomMenuOpen = true;
            return;
        }

        isBottomMenuOpen = true;

        MoveBottomMenu(() =>
        {
            currentType = type;
            inventoryScrollPanel.SetItems(currentType);
            MoveBottomMenu(() => LockButton(false));
        });
    }

    private void Start()
    {
        inventoryScrollPanel.SetItems(CosmeticTypes.Body);
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
    }
}