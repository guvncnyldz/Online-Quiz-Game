using System;
using System.Collections;
using System.Collections.Generic;
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

    public void AddQuestion(Question question)
    {
        questionPool.Push(question);
    }

    public int GetQuestionCount()
    {
        return questionPool.Count;
    }

    public void NewQuestion()
    {
        Question question = new Question();
        question.id = "1";
        question.question = "Türkiye'nin başkenti neresidir?";
        question.answers[0] = "Edirne";
        question.answers[1] = "Ankara";
        question.answers[2] = "İzmir";
        question.answers[3] = "İstanbul";
        AddQuestion(question);
        question = new Question();
        question.id = "2";
        question.question = "12+12 işleminin sonucu kaçtır?";
        question.answers[0] = "23";
        question.answers[1] = "22";
        question.answers[2] = "24";
        question.answers[3] = "25";
        AddQuestion(question);
        question = new Question();
        question.id = "3";
        question.question = "Cumhuriyet bayramı hangi tahrite kutlanılır?";
        question.answers[0] = "30 Ağustos";
        question.answers[1] = "23 Nisan";
        question.answers[2] = "19 Kasım";
        question.answers[3] = "29 Ekim";
        AddQuestion(question);
    }
}