using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrainingManager : QuestionBase
{
    private GameObject menuPopup;

    protected override void Awake()
    {
        base.Awake();

        menuPopup = FindObjectOfType<MenuPopup>().gameObject;
        menuPopup.SetActive(false);
        QuestionHTTP.GetQuestion(null, 10 - QuestionPool.GetInstance.GetQuestionCount());

        FindObjectOfType<Countdown>().StartCountDown(BeginGetQuestion);
    }

    public override void Pass()
    {
        base.Pass();

        menuPopup.SetActive(false);

        Observable.Timer(TimeSpan.FromSeconds(1.5f)).Subscribe(_ =>
        {
            timer.RestartCountdown();
            EndGetQuestion();
        });
    }

    protected override void BeginGetQuestion()
    {
        if (QuestionPool.GetInstance.GetQuestionCount() <= 4)
        {
            QuestionHTTP.GetQuestion(null, 6);
        }

        if (QuestionPool.GetInstance.GetQuestionCount() > 0)
        {
            EndGetQuestion();
        }
        else
        {
            QuestionHTTP.GetQuestion(EndGetQuestion);
        }
    }

    protected override void EndGetQuestion()
    {
        Question question = QuestionPool.GetInstance.GetQuestion();
        currentQuestion = question;

        questionPanel.Disappear(() =>
        {
            questionPanel.ChangeQuestion(question.question);
            questionPanel.Appear(() =>
            {
                timer.StartCountdown();
                joker.LockButton(false);
                menuPopup.SetActive(true);
            });
        });

        answerController.ChangeAnswer(question.answers);
    }

    public override void CheckAnswer(int answerId)
    {
        base.CheckAnswer(answerId);
        menuPopup.SetActive(false);

        answerController.ShowCorrect(currentQuestion.correct, answerId);

        if (answerId == currentQuestion.correct)
        {
            QuestionHTTP.Answer(currentQuestion, true);

            Observable.Timer(TimeSpan.FromSeconds(1.5f)).Subscribe(_ =>
            {
                correct++;
                timer.RestartCountdown();
                BeginGetQuestion();
            });
        }
        else
        {
            EndGame(0);
        }
    }

    protected override void TimesUp()
    {
        base.TimesUp();

        EndGame(0);
    }

    public override void EndGame(int extraCoin)
    {
        base.EndGame(extraCoin);
        menuPopup.SetActive(false);

        int earningCoin = 0;

        for (int i = 0; i < correct; i++)
        {
            earningCoin += Random.Range(1, 10);
        }

        User.GetInstance().Coin += earningCoin;
        ScoreHTTP.SaveScore(correct, earningCoin, (int) GameMods.training,1);
        
        Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
        {
            endPanel.gameObject.SetActive(true);
            endPanel.SetValues(earningCoin, correct);
        });
    }
}