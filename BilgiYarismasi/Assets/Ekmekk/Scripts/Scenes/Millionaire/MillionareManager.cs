using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MillionareManager : QuestionBase
{
    private MillionairePanel millionairePanel;
    protected override void Awake()
    {
        millionairePanel = FindObjectOfType<MillionairePanel>();

        base.Awake();

        QuestionHTTP.GetQuestion(() =>
        {
            Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
            {
                millionairePanel.Disappear(null);
                FindObjectOfType<Countdown>().StartCountDown(BeginGetQuestion);
            });
        }, 10 - QuestionPool.GetInstance.GetQuestionCount());
    }

    protected override void BeginGetQuestion()
    {
        Question question = QuestionPool.GetInstance.GetQuestion();
        currentQuestion = question;

        questionPanel.ChangeQuestion(question.question);

        questionPanel.Appear(() =>
        {
            timer.StartCountdown();
            joker.LockButton(false);
        });

        answerController.ChangeAnswer(question.answers);
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

            correct++;

            if (correct >= 10)
            {
                EndGame();
                return;
            }

            millionairePanel.Appear(() =>
            {
                millionairePanel.SetLevel(correct, () =>
                {
                    Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
                    {
                        millionairePanel.Disappear(() =>
                        {
                            timer.RestartCountdown();
                            EndGetQuestion();
                        });
                    });
                });
            });
        }
        else
        {
            correct = 0;
            millionairePanel.Appear(() =>
            {
                millionairePanel.FallIndicator();
                EndGame();
            });
        }
    }


    protected override void TimesUp()
    {
        base.TimesUp();
        
        correct = 0;
        millionairePanel.Appear(() =>
        {
            millionairePanel.FallIndicator();
            EndGame();
        });

        EndGame();
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
    void EndGame()
    {
        int earningCoin = millionairePanel.GetMoney(correct);

        ScoreHTTP.SaveScore(correct,earningCoin,2);

        Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
        {
            endPanel.gameObject.SetActive(true);
            endPanel.SetValues(earningCoin, correct);
        });
    }
}