﻿using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketScrollPanel : MonoBehaviour
{
    private EventTrigger eventTrigger;
    private TextMeshProUGUI txt_button;

    [SerializeField] private Button buttonRunner;
    [SerializeField] private TextMeshProUGUI txt_money, txt_gold, txt_name;
    [SerializeField] private RectTransform chosenButton, scrollPanel;
    [SerializeField] private GameObject itemButton;

    private List<GameObject> itemButtons;
    private ScrollRect scrollRect;
    public MarketItemButton[] buttons;
    private Character character;
    private CosmeticData previewCosmetic;
    private MarketPopUp marketPopUp;
    private PopUp popUp;

    private bool isPopUpOpen;
    public Action onBuy;

    public void SetMarketItems(CosmeticTypes cosmeticTypes, bool isCosmetic = true)
    {
        List<Item> items = MarketData.instance.GetData(cosmeticTypes);

        if (itemButtons.Count > 0)
        {
            foreach (GameObject button in itemButtons)
            {
                DestroyImmediate(button);
            }

            itemButtons.Clear();
        }

        foreach (Item item in items)
        {
            GameObject temp = Instantiate(itemButton, scrollPanel);
            temp.GetComponent<MarketItemButton>().Set(item, cosmeticTypes);
            itemButtons.Add(temp);
        }

        SetButtons();
        StartCoroutine(SetScrollPosSpecify());
    }

    void Awake()
    {
        marketPopUp = FindObjectOfType<MarketPopUp>();
        popUp = FindObjectOfType<PopUp>();

        previewCosmetic = new CosmeticData();
        previewCosmetic.SetCosmetic(User.GetInstance().cosmeticData);

        itemButtons = new List<GameObject>();
        scrollRect = GetComponent<ScrollRect>();
        character = FindObjectOfType<Character>();

        txt_button = buttonRunner.GetComponentInChildren<TextMeshProUGUI>();

        eventTrigger = GetComponentInChildren<EventTrigger>();

        EventTrigger.Entry pointerDrag = new EventTrigger.Entry();
        EventTrigger.Entry pointerUp = new EventTrigger.Entry();
        EventTrigger.Entry pointerDown = new EventTrigger.Entry();

        pointerDrag.eventID = EventTriggerType.Drag;
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerDown.eventID = EventTriggerType.PointerDown;

        pointerDown.callback.AddListener((data) =>
        {
            if (isPopUpOpen)
            {
                popUp.Disappear(0.1f);
                marketPopUp.Disappear(0.1f);
                isPopUpOpen = false;
            }
        });

        pointerDrag.callback.AddListener((data) => { GetClosestButton(); });
        pointerUp.callback.AddListener((data) => { SetScrollPos(); });

        eventTrigger.triggers.Add(pointerDrag);
        eventTrigger.triggers.Add(pointerUp);
        eventTrigger.triggers.Add(pointerDown);
    }

    public void SetButtons()
    {
        buttons = new MarketItemButton[scrollPanel.childCount];
        buttons = scrollPanel.GetComponentsInChildren<MarketItemButton>();
    }

    public void GetClosestButton()
    {
        if(buttons.Length == 0)
            return;
        
        float currentDis = Mathf.Infinity;
        MarketItemButton closestButton = buttons[0];

        foreach (MarketItemButton button in buttons)
        {
            float dis = (button.transform.position - chosenButton.position).sqrMagnitude;

            if (currentDis > dis)
            {
                currentDis = dis;
                closestButton = button;
            }
        }


        if (!closestButton.isBought)
        {
            txt_button.text = "Satın Al";
        }
        else
        {
            if (User.GetInstance().cosmeticData.body == closestButton.id)
                txt_button.text = "Kuşanıldı";
            else if (User.GetInstance().cosmeticData.footLeft == closestButton.id)
                txt_button.text = "Kuşanıldı";
            else if (User.GetInstance().cosmeticData.hair == closestButton.id)
                txt_button.text = "Kuşanıldı";
            else if (User.GetInstance().cosmeticData.handLeft == closestButton.id)
                txt_button.text = "Kuşanıldı";
            else if (User.GetInstance().cosmeticData.eye == closestButton.id)
                txt_button.text = "Kuşanıldı";
            else
                txt_button.text = "Kuşan";
        }

        txt_money.text = closestButton.money.ToString();
        txt_gold.text = closestButton.gold.ToString();
        txt_name.text = closestButton.name;
    }

    void Preview(MarketItemButton marketItemButton)
    {
        switch (marketItemButton.type)
        {
            case CosmeticTypes.Body:
                previewCosmetic.body = marketItemButton.id;
                break;
            case CosmeticTypes.Eye:
                previewCosmetic.eye = marketItemButton.id;
                break;
            case CosmeticTypes.Hand:
                previewCosmetic.handLeft = previewCosmetic.handRight = marketItemButton.id;
                break;
            case CosmeticTypes.Foot:
                previewCosmetic.footLeft = previewCosmetic.footRight = marketItemButton.id;
                break;
            case CosmeticTypes.Hair:
                previewCosmetic.hair = marketItemButton.id;
                break;
        }

        character.cosmetic.SetCosmetic(previewCosmetic);
    }

    public void SetScrollPos()
    {
        if(buttons.Length == 0)
            return;
        
        float currentDis = Mathf.Infinity;
        MarketItemButton closestButton = buttons[0];

        foreach (MarketItemButton button in buttons)
        {
            Vector3 dir = (button.transform.position - chosenButton.position);
            float dis = dir.sqrMagnitude;
            if (currentDis > dis)
            {
                currentDis = dis;
                closestButton = button;
            }
        }

        Vector3 dirClosest = (closestButton.transform.position - chosenButton.position);
        Vector3 pos = scrollPanel.position;
        pos.x -= dirClosest.x;
        scrollPanel.position = pos;

        if (!closestButton.isBought)
        {
            txt_button.text = "Satın Al";
        }
        else
        {
            if (User.GetInstance().cosmeticData.body == closestButton.id)
                txt_button.text = "Kuşanıldı";
            else if (User.GetInstance().cosmeticData.footLeft == closestButton.id)
                txt_button.text = "Kuşanıldı";
            else if (User.GetInstance().cosmeticData.hair == closestButton.id)
                txt_button.text = "Kuşanıldı";
            else if (User.GetInstance().cosmeticData.handLeft == closestButton.id)
                txt_button.text = "Kuşanıldı";
            else if (User.GetInstance().cosmeticData.eye == closestButton.id)
                txt_button.text = "Kuşanıldı";
            else
                txt_button.text = "Kuşan";
        }

        txt_money.text = closestButton.money.ToString();
        txt_gold.text = closestButton.gold.ToString();
        txt_name.text = closestButton.name;
        buttonRunner.onClick.RemoveAllListeners();
        buttonRunner.onClick.AddListener(() =>
        {
            RunOperation(closestButton);
        });


        scrollRect.velocity = Vector2.zero;

        Preview(closestButton);
    }

    private IEnumerator SetScrollPosSpecify()
    {
        yield return null;

        if(buttons.Length == 0)
            yield break;
        
        MarketItemButton choosenButton = buttons[0];

        foreach (MarketItemButton button in buttons)
        {
            switch (button.type)
            {
                case CosmeticTypes.Body:

                    if (button.id == previewCosmetic.body)
                    {
                        choosenButton = button;
                    }

                    break;
                case CosmeticTypes.Eye:
                    if (button.id == previewCosmetic.eye)
                    {
                        choosenButton = button;
                    }

                    break;
                case CosmeticTypes.Hand:
                    if (button.id == previewCosmetic.handLeft)
                    {
                        choosenButton = button;
                    }

                    break;
                case CosmeticTypes.Foot:
                    if (button.id == previewCosmetic.footLeft)
                    {
                        choosenButton = button;
                    }

                    break;
                case CosmeticTypes.Hair:
                    if (button.id == previewCosmetic.hair)
                    {
                        choosenButton = button;
                    }

                    break;
            }
        }

        Vector3 dirClosest = (choosenButton.transform.position - chosenButton.position);
        Vector3 pos = scrollPanel.position;
        pos.x -= dirClosest.x;
        scrollPanel.position = pos;

        if (!choosenButton.isBought)
        {
            txt_button.text = "Satın Al";
        }
        else
        {
            if (User.GetInstance().cosmeticData.body == choosenButton.id)
                txt_button.text = "Kuşanıldı";
            else if (User.GetInstance().cosmeticData.footLeft == choosenButton.id)
                txt_button.text = "Kuşanıldı";
            else if (User.GetInstance().cosmeticData.hair == choosenButton.id)
                txt_button.text = "Kuşanıldı";
            else if (User.GetInstance().cosmeticData.handLeft == choosenButton.id)
                txt_button.text = "Kuşanıldı";
            else if (User.GetInstance().cosmeticData.eye == choosenButton.id)
                txt_button.text = "Kuşanıldı";
            else
                txt_button.text = "Kuşan";
        }

        txt_money.text = choosenButton.money.ToString();
        txt_gold.text = choosenButton.gold.ToString();
        txt_name.text = choosenButton.name;
        buttonRunner.onClick.RemoveAllListeners();
        buttonRunner.onClick.AddListener(() =>
        {
            RunOperation(choosenButton);
        });

        scrollRect.velocity = Vector2.zero;

        Preview(choosenButton);
    }

    void RunOperation(MarketItemButton choosenButton)
    {
        isPopUpOpen = true;

        if (!choosenButton.isBought)
        {
            if (User.GetInstance().Coin >= choosenButton.gold && User.GetInstance().Money >= choosenButton.money)
            {
                marketPopUp.ResetListenerFromButton();
                marketPopUp.AddListenerToButton(() =>
                {
                    isPopUpOpen = false;
                    marketPopUp.Disappear();
                    choosenButton.Use(() => onBuy?.Invoke());
                    SetScrollPos();
                }).SetAndShow(choosenButton.money, choosenButton.gold);
            }
            else
            {
                popUp.ResetListenerFromButton();
                popUp.AddListenerToButton(() => isPopUpOpen = false)
                    .SetAndShow("Üzgünüm", "Yeterli paraya sahip değilsin");
            }
        }
        else
        {
            choosenButton.Use(null);
            SetScrollPos();
        }
    }
}