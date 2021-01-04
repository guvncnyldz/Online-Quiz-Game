using System;
using DG.Tweening;
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
        transform.DOShakePosition(1);
        text = text.Replace(@"\n", Environment.NewLine);
        error.text = text;
    }
}