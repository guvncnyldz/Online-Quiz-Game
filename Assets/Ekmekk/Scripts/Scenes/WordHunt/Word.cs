using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class Word
{
    public int length;
    public string word;

    public void Set(JToken jWord)
    {
        word = jWord["word"].ToString().ToUpper();
        length = word.Length;
    }
}