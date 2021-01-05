using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MillionairePanel : MonoBehaviour
{
    private Vector2 startPos;
    private RectTransform panelRect;
    [SerializeField] private RectTransform indicator;
    [SerializeField] private RectTransform[] levels;

    void Awake()
    {
        panelRect = GetComponent<RectTransform>();
        startPos = panelRect.anchoredPosition;
    }

    public void SetLevel(int level, Action onComplete)
    {
        if (level >= levels.Length)
            level = levels.Length - 1;

        indicator.DOAnchorPosY(levels[level].anchoredPosition.y, 0.75f).OnComplete(() => onComplete?.Invoke());
    }

    public void FallIndicator()
    {
        int[] rotateDir = {-1, 1};
        int dir = rotateDir[Random.Range(0, 2)];

        Vector3 eulerAngles = transform.eulerAngles;

        Vector3 posTarget = new Vector3(0, -800, 0);
        indicator.DOAnchorPos(posTarget, 0.5f).SetEase(Ease.InCubic);

        Vector3 rotateTarget = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z + 75 * dir);
        indicator.DORotate(rotateTarget, 0.5f)
            .SetEase(Ease.InCubic).OnComplete(() => { transform.eulerAngles = new Vector3(0, 0, 0); });
    }

    public int GetMoney(int level)
    {
        if (level >= levels.Length)
            level = levels.Length - 1;

        if (level == 0)
        {
            return 0;
        }

        return Convert.ToInt16(levels[level].GetComponent<TextMeshProUGUI>().text);
    }

    public void Disappear(Action onComplete)
    {
        panelRect.DOAnchorPosX(startPos.x * -1, 0.5f).OnComplete(() => onComplete?.Invoke())
            .SetEase(Ease.Linear);
    }

    public void Appear(Action onComplete)
    {
        panelRect.DOAnchorPosX(startPos.x, 0.5f).SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
    }
}