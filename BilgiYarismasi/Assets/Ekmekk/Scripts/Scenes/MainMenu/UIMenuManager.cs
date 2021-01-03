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
    [SerializeField] private Image background, bottomMenu, settingsButton, shopButton, energyBar, topMenu, playButton;
    [SerializeField] private RectTransform topRect, bottomRect, gameTypeRect;

    private Button[] buttons;

    public void Awake()
    {
        buttons = FindObjectsOfType<Button>();

        background.sprite = raceMenuSpriteses[User.GetInstance().Race].background;
        bottomMenu.sprite = raceMenuSpriteses[User.GetInstance().Race].bottomMenu;
        settingsButton.sprite = raceMenuSpriteses[User.GetInstance().Race].settingsButton;
        shopButton.sprite = raceMenuSpriteses[User.GetInstance().Race].shopButton;
        energyBar.sprite = raceMenuSpriteses[User.GetInstance().Race].energyBar;
        topMenu.sprite = raceMenuSpriteses[User.GetInstance().Race].topMenu;
        playButton.sprite = raceMenuSpriteses[User.GetInstance().Race].playButton;

        MoveTop();
        MoveBottom();
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
    public Sprite background, bottomMenu, settingsButton, shopButton, energyBar, topMenu, playButton;
}