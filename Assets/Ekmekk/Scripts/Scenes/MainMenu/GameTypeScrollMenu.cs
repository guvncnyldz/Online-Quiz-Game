﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameTypeScrollMenu : MonoBehaviour
{
    private EventTrigger eventTrigger;
    private Button buttonRunner;
    private TextMeshProUGUI txt_button, txt_info;

    [SerializeField] private RectTransform chosenButton, scrollPanel;
    [SerializeField] private ScrollMenuButton[] buttons;

    void Awake()
    {
        buttonRunner = GetComponentInChildren<Button>();
        txt_button = buttonRunner.GetComponentInChildren<TextMeshProUGUI>();
        txt_info = GetComponentInChildren<TextMeshProUGUI>();
        
        eventTrigger = GetComponentInChildren<EventTrigger>();

        EventTrigger.Entry pointerDrag = new EventTrigger.Entry();
        EventTrigger.Entry pointerUp = new EventTrigger.Entry();

        pointerDrag.eventID = EventTriggerType.Drag;
        pointerUp.eventID = EventTriggerType.PointerUp;

        pointerDrag.callback.AddListener((data) => { GetClosestButton(); });
        pointerUp.callback.AddListener((data) => { SetScrollPos(); });

        eventTrigger.triggers.Add(pointerDrag);
        eventTrigger.triggers.Add(pointerUp);

        GetClosestButton();
        SetScrollPos();
    }

    public void GetClosestButton()
    {
        float currentDis = Mathf.Infinity;
        ScrollMenuButton closestButton = buttons[0];

        foreach (ScrollMenuButton button in buttons)
        {
            float dis = (button.transform.position - chosenButton.position).sqrMagnitude;

            if (currentDis > dis)
            {
                currentDis = dis;
                closestButton = button;
            }
        }

        txt_button.text = closestButton.buttonName;
        txt_info.text = closestButton.modInfo;
    }

    public void SetScrollPos()
    {
        float currentDis = Mathf.Infinity;
        ScrollMenuButton closestButton = buttons[0];

        foreach (ScrollMenuButton button in buttons)
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

        if (!closestButton.buttonName.Equals("Kilitli"))
        {
            buttonRunner.interactable = true;
            txt_button.text = closestButton.buttonName;
            txt_info.text = closestButton.modInfo;

            buttonRunner.onClick.RemoveAllListeners();
            buttonRunner.onClick.AddListener(() => { closestButton.OnClickEvent.Invoke(); });
        }
        else
        {
            buttonRunner.interactable = false;
            txt_button.text = closestButton.buttonName;
            txt_info.text = closestButton.modInfo;
        }
    }
}