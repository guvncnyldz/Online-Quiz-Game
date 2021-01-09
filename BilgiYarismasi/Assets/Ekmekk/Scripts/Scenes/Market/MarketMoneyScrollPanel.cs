using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketMoneyScrollPanel : MonoBehaviour
{
    private EventTrigger eventTrigger;

    [SerializeField] private Button buttonRunner;
    [SerializeField] private TextMeshProUGUI txt_name;
    [SerializeField] private RectTransform chosenButton, scrollPanel;

    private ScrollRect scrollRect;
    public MarketMoneyItemButton[] buttons;
    private MarketPopUp marketPopUp;
    private PopUp popUp;

    private bool isPopUpOpen;
    public Action onBuy;

    void Awake()
    {
        marketPopUp = FindObjectOfType<MarketPopUp>();
        popUp = FindObjectOfType<PopUp>();
        scrollRect = GetComponent<ScrollRect>();

        StartCoroutine(SetScrollPosStart());

        
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

    IEnumerator SetScrollPosStart()
    {
        yield return null;
        SetScrollPos();
    }

    public void GetClosestButton()
    {
        float currentDis = Mathf.Infinity;
        MarketMoneyItemButton closestButton = buttons[0];

        foreach (MarketMoneyItemButton button in buttons)
        {
            float dis = (button.transform.position - chosenButton.position).sqrMagnitude;

            if (currentDis > dis)
            {
                currentDis = dis;
                closestButton = button;
            }
        }

        txt_name.text = closestButton.count.ToString();
    }

    public void SetScrollPos()
    {
        float currentDis = Mathf.Infinity;
        MarketMoneyItemButton closestButton = buttons[0];

        foreach (MarketMoneyItemButton button in buttons)
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

        txt_name.text = closestButton.count.ToString();

        buttonRunner.onClick.RemoveAllListeners();
        buttonRunner.onClick.AddListener(() => { RunOperation(closestButton); });

        scrollRect.velocity = Vector2.zero;
    }

    void RunOperation(MarketMoneyItemButton choosenButton)
    {
        isPopUpOpen = true;
        choosenButton.Use(() => onBuy?.Invoke());
    }
}