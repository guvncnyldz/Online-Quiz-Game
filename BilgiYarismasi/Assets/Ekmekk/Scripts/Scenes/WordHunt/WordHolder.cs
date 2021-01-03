using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordHolder : MonoBehaviour
{
    private Image img_mark;
    private TextMeshProUGUI txt_word;
    public string word;

    private void Awake()
    {
        txt_word = GetComponentInChildren<TextMeshProUGUI>();
        img_mark = GetComponentInChildren<Image>();
        img_mark.gameObject.SetActive(false);
    }

    public void SetWord(string word)
    {
        gameObject.SetActive(true);
        txt_word.text = word;
        this.word = word;
    }

    public void Mark()
    {
        img_mark.gameObject.SetActive(true);
    }
}