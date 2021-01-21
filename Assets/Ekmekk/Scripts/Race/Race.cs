using UnityEngine;

public class Race
{
    public int id;
    public string name;
    public string detail;
    public Color color;

    public Race(int id, string name,string detail,Color color)
    {
        this.id = id;
        this.name = name;
        this.detail = detail;
        this.color = color;
    }
}