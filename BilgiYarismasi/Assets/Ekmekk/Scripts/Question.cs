using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    public string id;
    public string question;
    public string[] answers;

    public Question()
    {
        answers = new string[4];
    }
}