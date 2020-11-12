using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Races
{
    public static Dictionary<RacesIndex, Race> races = new Dictionary<RacesIndex, Race>()
    {
        {RacesIndex.Fire, new Race(0, "ATEŞ", "Bilgilerini ateşin gücüyle birleştir", GetColor("#FF4400"))},
        {RacesIndex.Air, new Race(1, "HAVA", "Kasırgalarının önünde hiçbir soru duramayacak", GetColor("#65C1DB"))},
        {RacesIndex.Water, new Race(2, "SU", "Su, bilgelik anlamına gelir. Gücünü göster", GetColor("#0077BC"))},
        {RacesIndex.Earth, new Race(3, "TOPRAK", "Doğa tüm kudretiyle arkanda duracak", GetColor("#B67E64"))},
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
    Fire = 0,
    Air,
    Water,
    Earth
}