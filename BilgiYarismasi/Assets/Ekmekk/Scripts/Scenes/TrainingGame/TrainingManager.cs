using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    private QuestionPanel questionPanel;
    private AnswerController answerController;
    private Joker joker;
    private Timer timer;
    private EndPanel endPanel;
    public Question currentQuestion;

    private int correct;

    public void Awake()
    {
        correct = 0;

        joker = FindObjectOfType<Joker>();
        endPanel = FindObjectOfType<EndPanel>();
        questionPanel = FindObjectOfType<QuestionPanel>();
        answerController = FindObjectOfType<AnswerController>();
        timer = FindObjectOfType<Timer>();

        answerController.OnAnswer += CheckAnswer;
        timer.OnTimesUp += TimesUp;

        QuestionHTTP.GetQuestion(null, 10 - QuestionPool.GetInstance.GetQuestionCount());

        FindObjectOfType<Countdown>().StartCountDown(BeginGetQuestion);
    }

    void BeginGetQuestion()
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

    void EndGetQuestion()
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
        BeginGetQuestion();
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

            Observable.Timer(TimeSpan.FromSeconds(1.5f)).Subscribe(_ =>
            {
                correct++;
                timer.RestartCountdown();
                BeginGetQuestion();
            });
        }
        else
        {
            int earningCoin = correct * 7;

            User.GetInstance().Coin += earningCoin;
            QuestionHTTP.Answer(currentQuestion, false);

            Observable.Timer(TimeSpan.FromSeconds(1.5f)).Subscribe(_ =>
            {
                endPanel.gameObject.SetActive(true);
                endPanel.SetValues(earningCoin, correct);
            });
        }
    }

    void TimesUp()
    {
        questionPanel.Fall();
        answerController.Fall();
        joker.Fall();

        answerController.LockAnswers(false);
        joker.LockButton(true);
        
        int earningCoin = correct * 7;
        User.GetInstance().Coin += earningCoin;

        Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
        {
            endPanel.gameObject.SetActive(true);
            endPanel.SetValues(earningCoin, correct);
        });
    }
}