using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MillionareManager : QuestionBase
{
    private MillionairePanel millionairePanel;

    private GameObject menuPopup;

    [SerializeField] private EndPanel normalEndPanel, winEndPanel;
    [SerializeField] private GameObject golds;
    protected override void Awake()
    {
        menuPopup = FindObjectOfType<MenuPopup>().gameObject;
        menuPopup.SetActive(false);

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
            menuPopup.SetActive(true);
        });

        answerController.ChangeAnswer(question.answers);
    }

    protected override void EndGetQuestion()
    {
        if (QuestionPool.GetInstance.GetQuestionCount() <= 4)
        {
            QuestionHTTP.GetQuestion(null, 6);
        }

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

            correct++;

            if (correct >= 10)
            {
                EndGame(1);
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
                            menuPopup.gameObject.SetActive(true);
                            timer.RestartCountdown();
                            EndGetQuestion();
                        });
                    });
                });
            });
        }
        else
        {
            QuestionHTTP.Answer(currentQuestion, false);

            correct = 0;
            EndGame(correct);
        }
    }


    protected override void TimesUp()
    {
        base.TimesUp();

        correct = 0;
        EndGame(0);
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

    public override void EndGame(int extraCoin)
    {
        base.EndGame(extraCoin);

        menuPopup.SetActive(false);

        if (extraCoin == 0)
        {
            Fail();
        }
        else
        {
            if (correct <= 2)
            {
                correct = 0;
                Fail();
            }
            else if (correct > 2 && correct <= 5)
            {
                correct = 2;
                Win();
            }
            else if (correct > 5 && correct < 10)
            {
                correct = 5;
                Win();
            }
            else
            {
                Win();
            }
        }
    }

    void Fail()
    {
        millionairePanel.Appear(() =>
        {
            millionairePanel.FallIndicator();

            int earningCoin = millionairePanel.GetMoney(correct);

            ScoreHTTP.SaveScore(correct, earningCoin, 2, 0);

            Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
            {
                endPanel.gameObject.SetActive(true);
                normalEndPanel.SetValues(earningCoin, correct, false);
            });
        });
    }

    void Win()
    {
        millionairePanel.Appear(() =>
        {
            millionairePanel.SetLevel(correct, () =>
            {
                int earningCoin = millionairePanel.GetMoney(correct);

                ScoreHTTP.SaveScore(correct, earningCoin, (int) GameMods.millionaire, 1);

                if (correct == 10)
                {
                    golds.SetActive(true);
                    Destroy(golds,3);
                }
 
                Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
                {
                    endPanel.gameObject.SetActive(true);
                    if (correct == 10)
                    {
                        winEndPanel.SetValues(earningCoin, correct, true);
                    }
                    else
                    {
                        normalEndPanel.SetValues(earningCoin, correct, true);
                    }
                });
            });
        });
    }
}