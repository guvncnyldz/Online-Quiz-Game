using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WordHolderPanel : MonoBehaviour
{
    const float INPOSY = -228.5525f;
    const float OUTPOSY = -507;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ChangeWordHolderPanel(Action onUp)
    {
        rectTransform.DOAnchorPos(new Vector2(0, OUTPOSY), 0.25f).OnComplete(() =>
            {
                onUp?.Invoke();
                rectTransform.DOAnchorPos(new Vector2(0, INPOSY), 0.25f).SetEase(Ease.Linear).SetDelay(0.25f);
            }
        ).SetEase(Ease.Linear);
    }
}