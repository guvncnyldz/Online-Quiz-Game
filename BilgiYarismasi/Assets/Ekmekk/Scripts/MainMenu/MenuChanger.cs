using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuChanger : MonoBehaviour
{
    private const int PANELY = 113;

    [SerializeField] private MenuOption[] menuOptions;

    [SerializeField]
    private Image img_topmenu, img_bottommenu, img_settingsbutton, img_shopbutton, img_playbutton, background;
    [SerializeField] private TextMeshProUGUI txt_diomand, txt_gold, txt_play;

    private RectTransform bottomPanel, topPanel;

    public void Awake()
    {
        bottomPanel = img_bottommenu.GetComponent<RectTransform>();
        topPanel = img_topmenu.GetComponent<RectTransform>();

        bottomPanel.anchoredPosition = new Vector2(0, -1 * PANELY);
        topPanel.anchoredPosition = new Vector2(0, PANELY);

        int raceIndex = (int) User.GetInstance.race;
        MenuOption menuOption = menuOptions[raceIndex];

        img_bottommenu.sprite = menuOption.bottomMenu;
        img_topmenu.sprite = menuOption.topMenu;
        img_settingsbutton.sprite = menuOption.settingsButton;
        img_shopbutton.sprite = menuOption.shopButton;
        img_playbutton.sprite = menuOption.playButton;
        background.color = Races.races[(RacesIndex) raceIndex].color;

        txt_diomand.font = txt_gold.font = menuOption.coinFont;
        txt_play.font = menuOption.textFont;
        
        OpenMenu();
    }

    void OpenMenu()
    {
        bottomPanel.DOAnchorPos(new Vector2(0, PANELY), 1).SetEase(Ease.Linear);
        topPanel.DOAnchorPos(new Vector2(0, PANELY * -1), 1).SetEase(Ease.Linear);
    }

    void Test(int i)
    {
        int raceIndex = i;
        MenuOption menuOption = menuOptions[raceIndex];

        img_bottommenu.sprite = menuOption.bottomMenu;
        img_topmenu.sprite = menuOption.topMenu;
        img_settingsbutton.sprite = menuOption.settingsButton;
        img_shopbutton.sprite = menuOption.shopButton;
        img_playbutton.sprite = menuOption.playButton;
        background.color = Races.races[(RacesIndex) raceIndex].color;

        txt_diomand.font = txt_gold.font = menuOption.coinFont;
        txt_play.font = menuOption.textFont;
    }
}

[Serializable]
public class MenuOption
{
    public Sprite topMenu, bottomMenu, settingsButton, shopButton, playButton;
    public TMP_FontAsset coinFont;
    public TMP_FontAsset textFont;
}