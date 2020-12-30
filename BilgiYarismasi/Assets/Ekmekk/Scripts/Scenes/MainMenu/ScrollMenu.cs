using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollMenu : MonoBehaviour
{
    private EventTrigger eventTrigger;
    private Button buttonRunner;
    private TextMeshProUGUI txt_button;

    [SerializeField] private RectTransform chosenButton, scrollPanel;
    [SerializeField] private ScrollMenuButton[] buttons;

    void Awake()
    {
        buttonRunner = GetComponentInChildren<Button>();
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

        txt_button.text = closestButton.ButtonName;
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
        
        if (!closestButton.ButtonName.Equals("Kilitli"))
        {
            buttonRunner.interactable = true;

            buttonRunner.onClick.AddListener(() => { closestButton.OnClickEvent.Invoke(); });
        }
        else
            buttonRunner.interactable = false;
    }
}