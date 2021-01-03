using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class QuestionBase : MonoBehaviour
{
    protected QuestionPanel questionPanel;
    protected AnswerController answerController;
    protected Joker joker;
    protected Timer timer;
    protected EndPanel endPanel;
    
    public Question currentQuestion;
    
    protected int correct;

    protected virtual void Awake()
    {
        correct = 0;

        joker = FindObjectOfType<Joker>();
        endPanel = FindObjectOfType<EndPanel>();
        questionPanel = FindObjectOfType<QuestionPanel>();
        answerController = FindObjectOfType<AnswerController>();
        timer = FindObjectOfType<Timer>();

        answerController.OnAnswer += CheckAnswer;
        timer.OnTimesUp += TimesUp;
    }

    public virtual void Pass()
    {
        joker.LockButton(true);
        answerController.LockAnswers(false);

        timer.StopCountdown();
    }

    public virtual void CheckAnswer(int answerId)
    {
        joker.LockButton(true);
        answerController.LockAnswers(false);
        timer.StopCountdown();
    }

    protected virtual void TimesUp()
    {
        questionPanel.Fall();
        answerController.Fall();
        joker.Fall();

        answerController.LockAnswers(false);
        joker.LockButton(true);

    }
    protected abstract void BeginGetQuestion();
    protected abstract void EndGetQuestion();
}