using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letterboard : MonoBehaviour
{
    public LetterButton[] letterButtons;
    private List<LetterButton> clickedButton;

    private CellDirection currentDir;

    private string word = "";

    public Action<string> OnButtonClickUp;

    private void Awake()
    {
        clickedButton = new List<LetterButton>();
        letterButtons = new LetterButton[100];

        for (int i = 0; i < 100; i++)
        {
            LetterButton letterButton = transform.GetChild(i).GetComponent<LetterButton>();
            letterButtons[i] = letterButton;
            letterButton.index = i;
            letterButton.OnClick += ButtonClick;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            BeginButtonClickUp();
        }
    }

    void ButtonClick(int index)
    {
        //Eğer ilk tıklama ise
        if (clickedButton.Count == 0)
        {
            AddClickedLetter(index);
            return;
        }

        int lastIndex = clickedButton[clickedButton.Count - 1].index;

        //Eğer ikinci tıklama ise yön belirlenir
        if (clickedButton.Count == 1 && !letterButtons[index].isClicked)
        {
            if (WordHuntGridUtil.IsButtonsSidebySide(index, lastIndex, ref currentDir))
            {
                AddClickedLetter(index);
            }
        }
        //Eğer üçüncü tıklama ise ve yön farklıysa yönü değiştir
        else if (clickedButton.Count == 2 && !letterButtons[index].isClicked)
        {
            if (WordHuntGridUtil.IsButtonsSidebySide(index, clickedButton[0].index, ref currentDir))
            {
                RemoveClickedLetter(clickedButton[clickedButton.Count - 1].index);
                lastIndex = clickedButton[0].index;

                AddClickedLetter(index);
            }
            else
            {
                if (WordHuntGridUtil.IsButtonSameDir(index, lastIndex, currentDir))
                {
                    AddClickedLetter(index);
                }
            }
        }
        //Diğer tıklamalar için
        else if (!letterButtons[index].isClicked && WordHuntGridUtil.IsButtonSameDir(index, lastIndex, currentDir))
        {
            AddClickedLetter(index);
        }
    }

    void AddClickedLetter(int index)
    {
        clickedButton.Add(letterButtons[index]);
        letterButtons[index].EndOnClick();
        word += letterButtons[index].letter;
    }

    void RemoveClickedLetter(int index)
    {
        clickedButton.Remove(letterButtons[index]);
        letterButtons[index].OnClickUp();
        word = word.Remove(word.Length - 1);
    }

    void BeginButtonClickUp()
    {
        OnButtonClickUp?.Invoke(word);
    }

    public void EndButtonClickUp(bool isScore)
    {
        if (!isScore)
        {
            foreach (LetterButton clickedButton in clickedButton)
            {
                clickedButton.OnClickUp();
            }
        }

        clickedButton.Clear();
        word = "";
    }

    public void Hint(int index)
    {
        letterButtons[index].Hint();
    }
}