using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    private static User instance;

    public static User GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new User();
            }

            return instance;
        }
    }

    public string nickname;
    public string email;
    public RacesIndex race;

    private User()
    {
    }
}