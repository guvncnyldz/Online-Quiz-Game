using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Races
{
    public static Dictionary<RacesIndex, Race> races = new Dictionary<RacesIndex, Race>()
    {
        {RacesIndex.Fire, new Race("ATEŞ", "Ateş huuu buumm yanarsınn cızzz", GetColor("#FF4400"))},
        {RacesIndex.Air, new Race("HAVA", "Hava fış fış fış fış her yer hava", GetColor("#65C1DB"))},
        {RacesIndex.Water, new Race("SU", "Günde 5 litre çok sağlıklı fazlası zarar oo", GetColor("#0077BC"))},
        {RacesIndex.Earth, new Race("TOPRAK", "tatatataaa mamamamnı mikrofon şov", GetColor("#B67E64"))},
  
    };

    //Util
    static Color GetColor(string code)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(code, out color);
        return color;
    }
}
public enum RacesIndex
{
    Fire,
    Air,
    Water,
    Earth
}