using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

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
        txt_letter.color = Color.white;
        isClicked = false;
    }

    public void SetLetter(char letter)
    {
        this.letter = letter;
        txt_letter.text = letter.ToString();
        isLetterSet = true;
    }
}