﻿using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryScrollPanel : MonoBehaviour
{
    private EventTrigger eventTrigger;
    private TextMeshProUGUI txt_button;

    [SerializeField] private Button buttonRunner;
    [SerializeField] private TextMeshProUGUI txt_name;
    [SerializeField] private RectTransform chosenButton, scrollPanel;
    [SerializeField] private GameObject itemButton;
    [SerializeField] private ItemData itemData;

    private List<GameObject> itemButtons;
    private ScrollRect scrollRect;
    public InventoryItemButton[] buttons;
    private Character character;
    private CosmeticData previewCosmetic;

    private bool isPopUpOpen;

    public void SetItems(CosmeticTypes cosmeticTypes, bool isCosmetic = true)
    {
        if (itemButtons.Count > 0)
        {
            foreach (GameObject button in itemButtons)
            {
                DestroyImmediate(button);
            }

            itemButtons.Clear();
        }

        foreach (Item item in itemData.items)
        {
            if (item.isFree && item.isFree && cosmeticTypes == item.cosmeticTypes)
            {
                GameObject temp = Instantiate(itemButton, scrollPanel);
                temp.GetComponent<InventoryItemButton>().Set(item);
                itemButtons.Add(temp);
            }
        }

        foreach (InventoryCosmetic inventoryCosmetic in User.GetInstance().inventorySystem.cosmetics)
        {
            if (inventoryCosmetic.type == cosmeticTypes)
            {
                foreach (Item item in itemData.items)
                {
                    if (item.id == inventoryCosmetic.sprite_id)
                    {
                        GameObject temp = Instantiate(itemButton, scrollPanel);
                        temp.GetComponent<InventoryItemButton>().Set(item);
                        itemButtons.Add(temp);
                    }
                }
            }
        }

        SetButtons();
        StartCoroutine(SetScrollPosSpecify());
    }

    void Awake()
    {
        previewCosmetic = new CosmeticData();
        previewCosmetic.SetCosmetic(User.GetInstance().cosmeticData);

        itemButtons = new List<GameObject>();
        scrollRect = GetComponent<ScrollRect>();
        character = FindObjectOfType<Character>();

        txt_button = buttonRunner.GetComponentInChildren<TextMeshProUGUI>();

        eventTrigger = GetComponentInChildren<EventTrigger>();

        EventTrigger.Entry pointerDrag = new EventTrigger.Entry();
        EventTrigger.Entry pointerUp = new EventTrigger.Entry();

        pointerDrag.eventID = EventTriggerType.Drag;
        pointerUp.eventID = EventTriggerType.PointerUp;

        pointerDrag.callback.AddListener((data) => { GetClosestButton(); });
        pointerUp.callback.AddListener((data) => { SetScrollPos(); });

        eventTrigger.triggers.Add(pointerDrag);
        eventTrigger.triggers.Add(pointerUp);
    }

    public void SetButtons()
    {
        buttons = new InventoryItemButton[scrollPanel.childCount];
        buttons = scrollPanel.GetComponentsInChildren<InventoryItemButton>();
    }

    public void GetClosestButton()
    {
        if (buttons.Length == 0)
            return;

        float currentDis = Mathf.Infinity;
        InventoryItemButton closestButton = buttons[0];

        foreach (InventoryItemButton button in buttons)
        {
            float dis = (button.transform.position - chosenButton.position).sqrMagnitude;

            if (currentDis > dis)
            {
                currentDis = dis;
                closestButton = button;
            }
        }

        if (User.GetInstance().cosmeticData.body == closestButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.footLeft == closestButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.head == closestButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.hair == closestButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.handLeft == closestButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.eye == closestButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else
            txt_button.text = "Kuşan";

        txt_name.text = closestButton.name;
    }

    void Preview(InventoryItemButton marketItemButton)
    {
        switch (marketItemButton.type)
        {
            case CosmeticTypes.Head:
                previewCosmetic.head = marketItemButton.sprite_id;
                break;
            case CosmeticTypes.Body:
                previewCosmetic.body = marketItemButton.sprite_id;
                break;
            case CosmeticTypes.Eye:
                previewCosmetic.eye = marketItemButton.sprite_id;
                break;
            case CosmeticTypes.Hand:
                previewCosmetic.handLeft = previewCosmetic.handRight = marketItemButton.sprite_id;
                break;
            case CosmeticTypes.Foot:
                previewCosmetic.footLeft = previewCosmetic.footRight = marketItemButton.sprite_id;
                break;
            case CosmeticTypes.Hair:
                previewCosmetic.hair = marketItemButton.sprite_id;
                break;
        }

        character.cosmetic.SetCosmetic(previewCosmetic);
    }

    public void SetScrollPos()
    {
        if (buttons.Length == 0)
            return;

        float currentDis = Mathf.Infinity;
        InventoryItemButton closestButton = buttons[0];

        foreach (InventoryItemButton button in buttons)
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


        if (User.GetInstance().cosmeticData.body == closestButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.footLeft == closestButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.head == closestButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.hair == closestButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.handLeft == closestButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.eye == closestButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else
            txt_button.text = "Kuşan";


        txt_name.text = closestButton.name;
        buttonRunner.onClick.RemoveAllListeners();
        buttonRunner.onClick.AddListener(() => { RunOperation(closestButton); });


        scrollRect.velocity = Vector2.zero;

        Preview(closestButton);
    }

    private IEnumerator SetScrollPosSpecify()
    {
        yield return null;

        if (buttons.Length == 0)
            yield break;

        InventoryItemButton choosenButton = buttons[0];

        foreach (InventoryItemButton button in buttons)
        {
            switch (button.type)
            {
                case CosmeticTypes.Head:

                    if (button.sprite_id == previewCosmetic.head)
                    {
                        choosenButton = button;
                    }

                    break;
                case CosmeticTypes.Body:

                    if (button.sprite_id == previewCosmetic.body)
                    {
                        choosenButton = button;
                    }

                    break;
                case CosmeticTypes.Eye:
                    if (button.sprite_id == previewCosmetic.eye)
                    {
                        choosenButton = button;
                    }

                    break;
                case CosmeticTypes.Hand:
                    if (button.sprite_id == previewCosmetic.handLeft)
                    {
                        choosenButton = button;
                    }

                    break;
                case CosmeticTypes.Foot:
                    if (button.sprite_id == previewCosmetic.footLeft)
                    {
                        choosenButton = button;
                    }

                    break;
                case CosmeticTypes.Hair:
                    if (button.sprite_id == previewCosmetic.hair)
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

        if (User.GetInstance().cosmeticData.body == choosenButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.footLeft == choosenButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.hair == choosenButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.head == choosenButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.handLeft == choosenButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else if (User.GetInstance().cosmeticData.eye == choosenButton.sprite_id)
            txt_button.text = "Kuşanıldı";
        else
            txt_button.text = "Kuşan";


        txt_name.text = choosenButton.name;
        buttonRunner.onClick.RemoveAllListeners();
        buttonRunner.onClick.AddListener(() => { RunOperation(choosenButton); });

        scrollRect.velocity = Vector2.zero;

        Preview(choosenButton);
    }

    void RunOperation(InventoryItemButton choosenButton)
    {
        choosenButton.Use();
        SetScrollPos();
    }
}