using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class LoginScenePanelAnimation : MonoBehaviour
{
    private RectTransform rectTransform;

    private Tween tween;

    private Vector3 startPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    public void GoBackToPlace()
    {
        if (tween.active)
        {
            tween.Kill();
            rectTransform.DORotate(new Vector3(0, 0, 0), 0.5f);
        }

        tween = rectTransform.DOAnchorPos(startPos, 0.5f);
    }

    //OnComplete geçici, sunucu geldikten sonra kaldırılacak
    public void Fall(Action OnComplete)
    {
        int[] rotateDir = {-1, 1};
        int dir = rotateDir[Random.Range(0, 2)];

        Vector3 eulerAngles = transform.eulerAngles;

        Vector3 posTarget = new Vector3(0, -800, 0);
        tween = rectTransform.DOAnchorPos(posTarget, 0.5f).SetEase(Ease.InCubic);

        Vector3 rotateTarget = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z + 75 * dir);
        rectTransform.DORotate(rotateTarget, 0.5f)
            .SetEase(Ease.InCubic).OnComplete(() =>
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                OnComplete?.Invoke();
            });
    }

    public void Shake()
    {
        rectTransform.DOShakePosition(0.5f, 5, 100);
    }
}