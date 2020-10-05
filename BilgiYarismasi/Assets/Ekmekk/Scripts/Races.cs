using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Races
{
    public static Dictionary<RacesIndex, Race> races = new Dictionary<RacesIndex, Race>()
    {
        {RacesIndex.Fire, new Race("ATEŞ", "Bilgilerini ateşin gücüyle birleştir", GetColor("#FF4400"))},
        {RacesIndex.Air, new Race("HAVA", "Kasırgalarının önünde hiçbir soru duramayacak", GetColor("#65C1DB"))},
        {RacesIndex.Water, new Race("SU", "Su, bilgelik anlamına gelir. Gücünü göster", GetColor("#0077BC"))},
        {RacesIndex.Earth, new Race("TOPRAK", "Doğa tüm kudretiyle arkanda duracak", GetColor("#B67E64"))},
  
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