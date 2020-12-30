using System;
using TMPro;
using UnityEngine;

public class ErrorText : MonoBehaviour
{
    private TextMeshProUGUI error;

    void Awake()
    {
        error = GetComponent<TextMeshProUGUI>();
        error.text = "";
    }

    public void SetError(string text)
    {
        text = text.Replace(@"\n", Environment.NewLine);
        error.text = text;
    }
}