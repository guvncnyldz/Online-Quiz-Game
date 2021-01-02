using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UserBase
{

    public string Username
    {
        get => username;
        set
        {
            username = value;
        }
    }

    public string Email
    {
        get => email;
        set
        {
            email = value;
        }
    }

    public int Race
    {
        get => race;
        set
        {
            race = value;
        }
    }

    public int Coin
    {
        get => coin;
        set
        {
            coin = value;
        }
    }

    public int Money
    {
        get => money;
        set
        {
            money = value;
        }
    }

    public int Energy
    {
        get => energy;
        set
        {
            energy = value;
        }
    }
}
