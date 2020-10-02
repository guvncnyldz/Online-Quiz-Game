using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    [SerializeField] private QuestionPanel questionPanel;
    [SerializeField] private AnswerController answerController;
    [SerializeField] private Timer timer;

    private Question currentQuestion;

    public void Awake()
    {
        questionPanel = FindObjectOfType<QuestionPanel>();
        answerController = FindObjectOfType<AnswerController>();
        timer = FindObjectOfType<Timer>();

        answerController.OnAnswer += CheckAnswer;
        questionPanel.OnQuestionIn += timer.StartCountdown;
        timer.OnTimesUp += TimesUp;
    }

    private void Start()
    {
        BeginGetQuestion();
    }

    void BeginGetQuestion()
    {
        if (QuestionPool.GetInstance.GetQuestionCount() > 0)
        {
            EndGetQuestion();
        }
        else
        {
            //Serverdan question iste
        }

        if (QuestionPool.GetInstance.GetQuestionCount() <= 4)
        {
            //Serverdan question iste
        }
    }

    void EndGetQuestion()
    {
        Question question = QuestionPool.GetInstance.GetQuestion();
        currentQuestion = question;

        questionPanel.ChangeQuestion(question.question);
        answerController.ChangeAnswer(question.answers);
    }

    void CheckAnswer(int answerId)
    {
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
    }

    void TimesUp()
    {
        questionPanel.Fall();
        answerController.Fall();
        
        answerController.LockAnswers(false);
    }
}