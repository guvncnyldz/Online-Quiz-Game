using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Races
{
    public static Dictionary<RacesIndex, string> colorCodes = new Dictionary<RacesIndex, string>()
    {
        {RacesIndex.Fire,"#FF4400"},
        {RacesIndex.Air,"#65C1DB"},
        {RacesIndex.Water,"#0077BC"},
        {RacesIndex.Earth,"#B67E64"},
    };
}
public enum RacesIndex
{
    Fire,
    Air,
    Water,
    Earth
}