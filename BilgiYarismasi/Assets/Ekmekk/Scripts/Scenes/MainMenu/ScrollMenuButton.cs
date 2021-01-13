using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScrollMenuButton : MonoBehaviour
{
    public string buttonName;
    public string modInfo;
    public ScrollButtonEvent OnClickEvent;
}

[Serializable]
public class ScrollButtonEvent : UnityEvent {}