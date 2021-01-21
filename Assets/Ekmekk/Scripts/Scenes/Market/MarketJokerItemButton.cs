using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketJokerItemButton : MonoBehaviour
{
    public int gold;
    public int count;
    public JokerType type;

    public async void Use(Action onBuy)
    {
        switch (type)
        {
            case JokerType.dodge:
                User.GetInstance().jokerData.Pass += count;
                break;
            case JokerType.instinct:
                User.GetInstance().jokerData.Correct += count;
                break;
            case JokerType.trap:
                User.GetInstance().jokerData.Bomb += count;
                break;
        }

        User.GetInstance().Coin -= gold;
        onBuy?.Invoke();
    }
}

public enum JokerType
{
    instinct,
    dodge,
    trap
}