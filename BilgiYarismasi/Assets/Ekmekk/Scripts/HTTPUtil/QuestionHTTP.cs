using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class QuestionHTTP
{
    public async static void GetQuestion(Action success, int count = 1)
    {
        if (count <= 0)
        {
            success?.Invoke();
            return;
        }

        var values = new Dictionary<string, string>
        {
            {"count", count.ToString()},
        };

        JArray response = await HTTPApiUtil.Post(values, "question", "getQuestion");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
            return;
        }

        QuestionPool.GetInstance.AddQuestion(response);
        success?.Invoke();
    }

    public async static void Answer(Question question, bool isCorrect)
    {
        var values = new Dictionary<string, string>
        {
            {"is_correct", isCorrect ? 1.ToString() : 0.ToString()},
            {"question_id", question.id},
        };

        JArray response = await HTTPApiUtil.Put(values, "question", "answer");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
        }
    }
}