using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MillionareManager : MonoBehaviour
{
    private QuestionPanel questionPanel;
    private AnswerController answerController;
    private Joker joker;
    private Timer timer;
    private EndPanel endPanel;
    private MillionairePanel millionairePanel;
    public Question currentQuestion;

    private int level;

    public void Awake()
    {
        level = 0;

        millionairePanel = FindObjectOfType<MillionairePanel>();
        joker = FindObjectOfType<Joker>();
        endPanel = FindObjectOfType<EndPanel>();
        questionPanel = FindObjectOfType<QuestionPanel>();
        answerController = FindObjectOfType<AnswerController>();
        timer = FindObjectOfType<Timer>();

        answerController.OnAnswer += CheckAnswer;
        timer.OnTimesUp += TimesUp;

        QuestionHTTP.GetQuestion(() =>
        {
            Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
            {
                millionairePanel.Disappear(null);
                FindObjectOfType<Countdown>().StartCountDown(StartMillionare);
            });
        }, 10 - QuestionPool.GetInstance.GetQuestionCount());
    }

    void StartMillionare()
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

    private void NextQuestion()
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

    public void Pass()
    {
        joker.LockButton(true);
        answerController.LockAnswers(false);

        timer.StopCountdown();
        timer.RestartCountdown();
        StartMillionare();
    }

    public void CheckAnswer(int answerId)
    {
        joker.LockButton(true);
        answerController.LockAnswers(false);
        timer.StopCountdown();

        answerController.ShowCorrect(currentQuestion.correct, answerId);

        if (answerId == currentQuestion.correct)
        {
            QuestionHTTP.Answer(currentQuestion, true);

            level++;

            if (level >= 10)
            {
                EndGame();
                return;
            }

            millionairePanel.Appear(() =>
            {
                millionairePanel.SetLevel(level, () =>
                {
                    Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
                    {
                        millionairePanel.Disappear(() =>
                        {
                            timer.RestartCountdown();
                            NextQuestion();
                        });
                    });
                });
            });
        }
        else
        {
            EndGame();
        }
    }


    void TimesUp()
    {
        questionPanel.Fall();
        answerController.Fall();
        joker.Fall();

        answerController.LockAnswers(false);
        joker.LockButton(true);

        EndGame();
    }

    void EndGame()
    {
        int earningCoin = millionairePanel.GetMoney(level);

        Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
        {
            endPanel.gameObject.SetActive(true);
            endPanel.SetValues(earningCoin, level);
        });
    }
}