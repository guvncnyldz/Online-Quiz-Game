using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnswerController : MonoBehaviour
{
    public Answer[] answers;
    private Button[] btn_answers;

    public Action<int> OnAnswer;

    public void Awake()
    {
        btn_answers = new Button[4];

        for (int i = 0; i < 4; i++)
        {
            btn_answers[i] = answers[i].GetComponent<Button>();
            answers[i].OnClickAnswer += CheckAnswer;
        }
    }

    //Serverdan sonra değişecek
    public void CheckAnswer(int buttonId)
    {
        LockAnswers(false);
        OnAnswer?.Invoke(buttonId);
    }

    public void Fall()
    {
        foreach (Answer answer in answers)
        {
            answer.Fall();
        }
    }
    public void ChangeAnswer(string[] answers)
    {
        for (int i = 0; i < 4; i++)
        {
            this.answers[i].ChangeAnswer(answers[i]);
        }
    }

    public void LockAnswers(bool isUnlocked)
    {
        foreach (Button btn_answer in btn_answers)
        {
            btn_answer.enabled = isUnlocked;
        }
    }

    public void ShowCorrect(int correctAnswer, int choosenAnswer)
    {
        if (correctAnswer == choosenAnswer)
        {
            answers[choosenAnswer].ResultAnswer(true);
        }
        else
        {
            answers[correctAnswer].ResultAnswer(true);
            answers[choosenAnswer].ResultAnswer(false);
        }
    }
}