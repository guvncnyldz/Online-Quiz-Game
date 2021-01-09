using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketJokerScrollPanel : MonoBehaviour
{
    private EventTrigger eventTrigger;

    [SerializeField] private Button buttonRunner;
    [SerializeField] private TextMeshProUGUI txt_gold, txt_name;
    [SerializeField] private RectTransform chosenButton, scrollPanel;

    private ScrollRect scrollRect;
    public MarketJokerItemButton[] buttons;
    private MarketPopUp marketPopUp;
    private PopUp popUp;

    private bool isPopUpOpen;
    public Action onBuy;

    void Awake()
    {
        marketPopUp = FindObjectOfType<MarketPopUp>();
        popUp = FindObjectOfType<PopUp>();
        scrollRect = GetComponent<ScrollRect>();

        eventTrigger = GetComponentInChildren<EventTrigger>();

        StartCoroutine(SetScrollPosStart());
        
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

    public void GetClosestButton()
    {
        float currentDis = Mathf.Infinity;
        MarketJokerItemButton closestButton = buttons[0];

        foreach (MarketJokerItemButton button in buttons)
        {
            float dis = (button.transform.position - chosenButton.position).sqrMagnitude;

            if (currentDis > dis)
            {
                currentDis = dis;
                closestButton = button;
            }
        }

        txt_gold.text = closestButton.gold.ToString();
        txt_name.text = "Sahip Olduğun: ";

        switch (closestButton.type)
        {
            case JokerType.dodge:
                txt_name.text += User.GetInstance().jokerData.Pass;
                break;
            case JokerType.instinct:
                txt_name.text += User.GetInstance().jokerData.Correct;
                break;
            case JokerType.trap:
                txt_name.text += User.GetInstance().jokerData.Bomb;
                break;
        }
    }

    public void SetScrollPos()
    {
        float currentDis = Mathf.Infinity;
        MarketJokerItemButton closestButton = buttons[0];

        foreach (MarketJokerItemButton button in buttons)
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

        txt_gold.text = closestButton.gold.ToString();
        txt_name.text = "Sahip Olduğun: ";

        switch (closestButton.type)
        {
            case JokerType.dodge:
                txt_name.text += User.GetInstance().jokerData.Pass;
                break;
            case JokerType.instinct:
                txt_name.text += User.GetInstance().jokerData.Correct;
                break;
            case JokerType.trap:
                txt_name.text += User.GetInstance().jokerData.Bomb;
                break;
        }

        buttonRunner.onClick.RemoveAllListeners();
        buttonRunner.onClick.AddListener(() => { RunOperation(closestButton); });


        scrollRect.velocity = Vector2.zero;
    }

    IEnumerator SetScrollPosStart()
    {
        yield return null;
        SetScrollPos();
    }

    void RunOperation(MarketJokerItemButton choosenButton)
    {
        isPopUpOpen = true;

        if (User.GetInstance().Coin >= choosenButton.gold)
        {
            marketPopUp.ResetListenerFromButton();
            marketPopUp.AddListenerToButton(() =>
            {
                isPopUpOpen = false;
                marketPopUp.Disappear();
                choosenButton.Use(() => onBuy?.Invoke());
                SetScrollPos();
            }).SetAndShow(0, choosenButton.gold);
        }
        else
        {
            popUp.ResetListenerFromButton();
            popUp.AddListenerToButton(() => isPopUpOpen = false)
                .SetAndShow("Üzgünüm", "Yeterli paraya sahip değilsin");
        }
    }
}