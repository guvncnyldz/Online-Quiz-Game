using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class LetterButton : MonoBehaviour
{
    public int index;
    public char letter;
    public bool isClicked;
    public bool isLetterSet;

    private TextMeshProUGUI txt_letter;
    private Image img_button;

    [SerializeField] private Sprite clicked, notClicked;

    public Action<int> OnClick;

    private bool isHint;

    private void Awake()
    {
        img_button = GetComponent<Image>();
        txt_letter = GetComponentInChildren<TextMeshProUGUI>();

        txt_letter.text = letter.ToString();
    }

    public void BeginOnClick()
    {
        OnClick?.Invoke(index);
    }

    public void EndOnClick()
    {
        img_button.sprite = clicked;
        txt_letter.color = Color.red;
        isClicked = true;
    }

    public void OnClickUp()
    {
        img_button.sprite = notClicked;
        if (isHint)
            txt_letter.color = Color.yellow;
        else
            txt_letter.color = Color.white;
        isClicked = false;
    }

    public void Hint()
    {
        isHint = true;
        txt_letter.color = Color.yellow;
    }

    public void SetLetter(char letter)
    {
        this.letter = letter;
        txt_letter.text = letter.ToString();
        isLetterSet = true;
    }

    public void Fall()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        int[] rotateDir = {-1, 1};
        int dir = rotateDir[Random.Range(0, 2)];

        Vector3 eulerAngles = transform.eulerAngles;

        Vector3 posTarget = new Vector3(0, transform.position.y - 3, 0);
        transform.DOMoveY(posTarget.y, 1.5f).SetEase(Ease.InCubic);

        Vector3 rotateTarget = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z + 75 * dir);
        rectTransform.DORotate(rotateTarget, 0.5f)
            .SetEase(Ease.InCubic).OnComplete(() => { transform.eulerAngles = new Vector3(0, 0, 0); });
    }
}