using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LetterBoardPanel : MonoBehaviour
{
    const float INPOSY = 164;
    const float OUTPOSY = 549;

    private RectTransform rectTransform;
    private GridLayoutGroup gridLayout;
    private void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void ChangeLetterPanel(Action OnUp, Action OnDown)
    {
        rectTransform.DOAnchorPos(new Vector2(0, OUTPOSY), 0.25f).OnComplete(() =>
            {
                OnUp?.Invoke();
                rectTransform.DOAnchorPos(new Vector2(0, INPOSY), 0.25f).SetEase(Ease.Linear).SetDelay(0.25f)
                    .OnComplete(
                        () =>
                        {
                            OnDown?.Invoke();
                        });
            }
        ).SetEase(Ease.Linear);
    }

    public void Fall()
    {
        gridLayout.enabled = false;

        Letterboard letterboard = GetComponent<Letterboard>();

        foreach (LetterButton letterButton in letterboard.letterButtons)
        {
            letterButton.Fall();
        }
    }
}