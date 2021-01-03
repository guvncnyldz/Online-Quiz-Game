using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class AnswerController : MonoBehaviour
{
    public Answer[] answers;
    private Button[] btn_answers;

    public Action<int> OnAnswer;

    [SerializeField] private Sprite[] raceJokerEffect;

    public void Awake()
    {
        btn_answers = new Button[4];

        for (int i = 0; i < 4; i++)
        {
            btn_answers[i] = answers[i].GetComponent<Button>();
            answers[i].OnClickAnswer += CheckAnswer;
        }
    }

    public void CheckAnswer(int buttonId)
    {
        OnAnswer?.Invoke(buttonId);
    }

    public void JokerEffect(int answer)
    {
        answers[answer].JokerEffect(raceJokerEffect[User.GetInstance().Race]);
    }

    public void Fall()
    {
        foreach (Answer answer in answers)
        {
            answer.Fall();
        }
    }

    public void ChangeAnswer(string[] newAnswers)
    {
        int j = 0;
        for (int i = 0; i < 4; i++)
        {
            answers[i].Disappear(() =>
            {
                answers[j].SetAnswer(newAnswers[j]);
                answers[j].Appear(null);
                j++;
            });
        }
    }

    public void Appear()
    {
        for (int i = 0; i < 4; i++)
        {
            answers[i].Appear(null);
        }
    }

    public void Disappear()
    {
        for (int i = 0; i < 4; i++)
        {
            answers[i].Disappear(null);
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
        if (choosenAnswer == -1)
        {
            answers[correctAnswer].ResultAnswer(true);
            return;
        }
        
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