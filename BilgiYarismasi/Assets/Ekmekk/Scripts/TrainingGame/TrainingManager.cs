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
    private TrainingEndPanel trainingEndPanel;
    public Question currentQuestion;

    private int correct;

    public void Awake()
    {
        correct = 0;

        joker = FindObjectOfType<Joker>();
        trainingEndPanel = FindObjectOfType<TrainingEndPanel>();
        questionPanel = FindObjectOfType<QuestionPanel>();
        answerController = FindObjectOfType<AnswerController>();
        timer = FindObjectOfType<Timer>();

        answerController.OnAnswer += CheckAnswer;
        questionPanel.OnQuestionIn += () =>
        {
            timer.StartCountdown();
            joker.LockButton(true);
        };
        timer.OnTimesUp += TimesUp;
    }

    private void Start()
    {
        BeginGetQuestion();
    }

    public void BeginGetQuestion()
    {
        if (QuestionPool.GetInstance.GetQuestionCount() > 0)
        {
            EndGetQuestion();
        }
        else
        {
            //Serverdan question iste
        }

        if (QuestionPool.GetInstance.GetQuestionCount() <= 0)
        {
            QuestionPool.GetInstance.NewQuestion();
        }
    }

    void EndGetQuestion()
    {
        Question question = QuestionPool.GetInstance.GetQuestion();
        currentQuestion = question;

        questionPanel.ChangeQuestion(question.question);
        answerController.ChangeAnswer(question.answers);
    }

    public void Pass()
    {
        joker.LockButton(false);
        timer.StopCountdown();
        timer.RestartCountdown();
        BeginGetQuestion();
    }

    public void CheckAnswer(int answerId)
    {
        joker.LockButton(false);
        timer.StopCountdown();
        //Servera sorar
        answerController.ShowCorrect(Convert.ToInt16(currentQuestion.id), answerId);
        if (answerId == Convert.ToInt16(currentQuestion.id))
        {
            Observable.Timer(TimeSpan.FromSeconds(1.5f)).Subscribe(_ =>
            {
                timer.RestartCountdown();
                BeginGetQuestion();
            });
        }
        else
        {
            Observable.Timer(TimeSpan.FromSeconds(1.5f)).Subscribe(_ =>
            {
                trainingEndPanel.gameObject.SetActive(true);
                trainingEndPanel.SetValues(correct * 7, correct);
            });
        }
    }

    void TimesUp()
    {
        questionPanel.Fall();
        answerController.Fall();
        joker.Fall();

        answerController.LockAnswers(false);
        joker.LockButton(false);

        Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ =>
        {
            trainingEndPanel.gameObject.SetActive(true);
            trainingEndPanel.SetValues(correct * 7, correct);
        });
    }
}