using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryJokerScrollPanel : MonoBehaviour
{
    private EventTrigger eventTrigger;

    [SerializeField] private TextMeshProUGUI txt_name;
    [SerializeField] private RectTransform chosenButton, scrollPanel;

    private ScrollRect scrollRect;
    public InventoryJokerItemButton[] buttons;

    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();

        eventTrigger = GetComponentInChildren<EventTrigger>();

        StartCoroutine(SetScrollPosStart());

        EventTrigger.Entry pointerDrag = new EventTrigger.Entry();
        EventTrigger.Entry pointerUp = new EventTrigger.Entry();

        pointerDrag.eventID = EventTriggerType.Drag;
        pointerUp.eventID = EventTriggerType.PointerUp;

        pointerDrag.callback.AddListener((data) => { GetClosestButton(); });
        pointerUp.callback.AddListener((data) => { SetScrollPos(); });

        eventTrigger.triggers.Add(pointerDrag);
        eventTrigger.triggers.Add(pointerUp);
    }

    public void GetClosestButton()
    {
        float currentDis = Mathf.Infinity;
        InventoryJokerItemButton closestButton = buttons[0];

        foreach (InventoryJokerItemButton button in buttons)
        {
            float dis = (button.transform.position - chosenButton.position).sqrMagnitude;

            if (currentDis > dis)
            {
                currentDis = dis;
                closestButton = button;
            }
        }

        txt_name.text = "Sahip Olduğun: " + closestButton.count;
    }

    public void SetScrollPos()
    {
        float currentDis = Mathf.Infinity;
        InventoryJokerItemButton closestButton = buttons[0];

        foreach (InventoryJokerItemButton button in buttons)
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

        txt_name.text = "Sahip Olduğun: " + closestButton.count;

        scrollRect.velocity = Vector2.zero;
    }

    IEnumerator SetScrollPosStart()
    {
        yield return null;
        SetScrollPos();
    }
}