using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class LastManManager : QuestionBase
{
    private bool isFirstQuestion;
    private PlayerPanel playerPanel;
    private int myAnswer;

    protected override void Awake()
    {
        base.Awake();

        playerPanel = FindObjectOfType<PlayerPanel>();
    }

    private void Start()
    {
        playerPanel.AppearPanel(true);
    }

    public void SetQuestion(Question question)
    {
        currentQuestion = question;
        BeginGetQuestion();
    }

    protected override void BeginGetQuestion()
    {
        if (isFirstQuestion)
        {
            FindObjectOfType<Countdown>().StartCountDown(BeginGetQuestion);
        }

        timer.RestartCountdown();
        EndGetQuestion();
    }

    protected override void EndGetQuestion()
    {
        playerPanel.AppearPanel(false);

        questionPanel.Disappear(() =>
        {
            questionPanel.ChangeQuestion(currentQuestion.question);
            questionPanel.Appear(() =>
            {
                timer.StartCountdown();
                joker.LockButton(false);
            });
        });

        answerController.ChangeAnswer(currentQuestion.answers);
    }

    public override void Pass()
    {
        base.Pass();

        myAnswer = -1;
        Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ => { playerPanel.AppearPanel(true); });
        JObject jObject = new JObject();
        jObject.Add("route", "lastman");
        jObject.Add("method", "answer");
        jObject.Add("answer", "-1");
        SocketUtil.ws.Send(jObject.ToString());
    }

    public override void CheckAnswer(int answerId)
    {
        base.CheckAnswer(answerId);
        myAnswer = answerId;
        playerPanel.AppearPanel(true);


        JObject jObject = new JObject();
        jObject.Add("route", "lastman");
        jObject.Add("method", "answer");
        jObject.Add("answer", answerId.ToString());
        SocketUtil.ws.Send(jObject.ToString());
    }

    public void Result(int answer)
    {
        answerController.ShowCorrect(answer, myAnswer);

        if (myAnswer == -1)
            return;

        if (answer == myAnswer)
        {
            QuestionHTTP.Answer(currentQuestion, true);
            correct++;

            if (playerPanel.userCount <= 1)
            {
                EndGame(Random.Range(750, 1000));
            }
        }
        else
        {
            QuestionHTTP.Answer(currentQuestion, false);

            EndGame(0);
        }
    }

    protected override void TimesUp()
    {
        base.TimesUp();

        EndGame(0);
    }

    public int GetCorrectCount()
    {
        return correct;
    }

    public void EndGame(int extraCoin)
    {
        Destroy(FindObjectOfType<LastManSocket>());

        int earningCoin = extraCoin;

        for (int i = 0; i < correct; i++)
        {
            earningCoin += Random.Range(1, 10);
        }
        ScoreHTTP.SaveScore(correct,earningCoin,1);

        Observable.Timer(TimeSpan.FromSeconds(2.5f)).Subscribe(_ =>
        {
            User.GetInstance().Coin += earningCoin;

            endPanel.gameObject.SetActive(true);
            endPanel.SetValues(earningCoin, correct);
        });
    }
}