using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UniRx;
using UnityEngine;

public class QuestionPool
{
    private static QuestionPool instance;

    public static QuestionPool GetInstance
    {
        get
        {
            if (instance == null)
                instance = new QuestionPool();

            return instance;
        }
    }

    private Stack<Question> questionPool;

    QuestionPool()
    {
        questionPool = new Stack<Question>();

        NewQuestion();
    }

    public Question GetQuestion()
    {
        Question question = questionPool.Pop();
        return question;
    }

    public void AddQuestion(JArray questions)
    {
        foreach (JToken jQuestion in questions)
        {
            Question question = new Question();
            question.Set(jQuestion);
            questionPool.Push(question);
        }
    }

    public int GetQuestionCount()
    {
        return questionPool.Count;
    }

    public void NewQuestion()
    {
    }
}