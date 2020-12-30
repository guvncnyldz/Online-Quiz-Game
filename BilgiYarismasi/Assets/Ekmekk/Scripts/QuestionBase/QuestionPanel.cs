using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuestionPanel : MonoBehaviour
{
    const float INPOSY = 157;
    const float OUTPOSY = 528;

    [SerializeField] private Sprite[] panelSprites;
    [SerializeField] private TextMeshProUGUI txt_question;

    private RectTransform rectTransform;
    private Image panelImage;

    public Action OnQuestionIn;
    private void Awake()
    {
        panelImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(0, OUTPOSY);
    }

    public void ChangeQuestion(string question)
    {
        txt_question.text = question;
        panelImage.sprite = panelSprites[Random.Range(0, panelSprites.Length)];

        rectTransform.DOAnchorPos(new Vector2(0, OUTPOSY), 0.25f).OnComplete(() =>
            {
                rectTransform.DOAnchorPos(new Vector2(0, INPOSY), 0.25f).SetEase(Ease.Linear).SetDelay(0.25f).OnComplete(
                    () =>
                    {
                        OnQuestionIn?.Invoke();
                    });
            }
        ).SetEase(Ease.Linear);
    }

    public void Disappear(Action onComplete)
    {
        rectTransform.DOAnchorPos(new Vector2(0, OUTPOSY), 0.25f).OnComplete(onComplete.Invoke).SetEase(Ease.Linear);
    }

    public void Appear(Action onComplete)
    {
        rectTransform.DOAnchorPos(new Vector2(0, INPOSY), 0.25f).SetEase(Ease.Linear).SetDelay(0.25f).OnComplete(onComplete.Invoke);
    }
    public void Fall()
    {
        int[] rotateDir = {-1, 1};
        int dir = rotateDir[Random.Range(0, 2)];

        Vector3 eulerAngles = transform.eulerAngles;

        Vector3 posTarget = new Vector3(0, -800, 0);
        rectTransform.DOAnchorPos(posTarget, 0.5f).SetEase(Ease.InCubic);

        Vector3 rotateTarget = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z + 75 * dir);
        rectTransform.DORotate(rotateTarget, 0.5f)
            .SetEase(Ease.InCubic).OnComplete(() => { transform.eulerAngles = new Vector3(0, 0, 0); });
    }
}