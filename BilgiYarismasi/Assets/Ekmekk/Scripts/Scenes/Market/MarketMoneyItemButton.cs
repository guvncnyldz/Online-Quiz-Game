using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketMoneyItemButton : MonoBehaviour
{
    public int count;

    public void Use(Action onUse)
    {
        User.GetInstance().Money += count;
        onUse?.Invoke();
    }
}
