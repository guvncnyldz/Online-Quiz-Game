using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrainingManager : QuestionBase
{
    protected override void Awake()
    {
        base.Awake();

        QuestionHTTP.GetQuestion(null, 10 - QuestionPool.GetInstance.GetQuestionCount());

        FindObjectOfType<Countdown>().StartCountDown(BeginGetQuestion);
    }

    public override void Pass()
    {
        base.Pass();
        
                
        Observable.Timer(TimeSpan.FromSeconds(1.5f)).Subscribe(_ =>
        {
            timer.RestartCountdown();
            EndGetQuestion();
        });
    }

    protected override void BeginGetQuestion()
    {
        if (QuestionPool.GetInstance.GetQuestionCount() > 0)
        {
            EndGetQuestion();
        }
        else
        {
            QuestionHTTP.GetQuestion(EndGetQuestion);
        }

        if (QuestionPool.GetInstance.GetQuestionCount() <= 4)
        {
            QuestionHTTP.GetQuestion(null, 6);
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
            });
        });

        answerController.ChangeAnswer(question.answers);
    }
    public override void CheckAnswer(int answerId)
    {
        base.CheckAnswer(answerId);

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
            int earningCoin = 0;
        
            for (int i = 0; i < correct; i++)
            {
                earningCoin += Random.Range(1, 10);
            }

            User.GetInstance().Coin += earningCoin;
            QuestionHTTP.Answer(currentQuestion, false);

            Observable.Timer(TimeSpan.FromSeconds(1.5f)).Subscribe(_ =>
            {
                endPanel.gameObject.SetActive(true);
                endPanel.SetValues(earningCoin, correct);
            });
        }
    }

    protected override void TimesUp()
    {
        base.TimesUp();

        int earningCoin = 0;
        
        for (int i = 0; i < correct; i++)
        {
            earningCoin += Random.Range(1, 10);
        }
        
        User.GetInstance().Coin += earningCoin;

        Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
        {
            endPanel.gameObject.SetActive(true);
            endPanel.SetValues(earningCoin, correct);
        });
    }
}