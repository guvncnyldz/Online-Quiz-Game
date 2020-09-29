using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class RaceDetail : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI raceName;

    public void SetDetail(string name, string colorName, string detail)
    {
        Color color = raceName.color;
        ColorUtility.TryParseHtmlString(colorName, out color);
        raceName.DOColor(color, 0);
        raceName.DOFade(0, 0);
        raceName.DOFade(1, 1);
        raceName.text = name;
    }
}