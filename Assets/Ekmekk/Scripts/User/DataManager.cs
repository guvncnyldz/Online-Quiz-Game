using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static int GameSeries
    {
        get => PlayerPrefs.GetInt("GameSeries", 0);
        set => PlayerPrefs.SetInt("GameSeries", value);
    }
}