using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryJokerItemButton : MonoBehaviour
{
    public int count;
    public JokerType type;

    public void Awake()
    {
        switch (type)
        {
            case JokerType.dodge:
                count = User.GetInstance().jokerData.Pass;
                break;
            case JokerType.instinct:
                count = User.GetInstance().jokerData.Correct;
                break;
            case JokerType.trap:
                count = User.GetInstance().jokerData.Bomb;
                break;
        }
    }
}
