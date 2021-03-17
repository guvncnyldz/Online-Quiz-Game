using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketMoneyItemButton : MonoBehaviour
{
    public int count;
    public string id;
    public void Use(Action onUse)
    {
        FindObjectOfType<IAP>().BuyConsumable(id, () =>
        {
            User.GetInstance().Money += count;
            onUse?.Invoke();
        });
    }
}
