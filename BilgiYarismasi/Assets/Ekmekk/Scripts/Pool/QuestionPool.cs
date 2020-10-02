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

        Question question = new Question();
        question.id = "1";
        question.question = "Türkiye'nin başkenti neresidir?";
        question.answers[0] = "Edirne";
        question.answers[1] = "bu";
        question.answers[2] = "Ankara";
        question.answers[3] = "İstanbul";
        AddQuestion(question);
        question = new Question();
        question.id = "2";
        question.question = "İşte bilmem ne?";
        question.answers[0] = "Aynen";
        question.answers[1] = "sdf";
        question.answers[2] = "bu";
        question.answers[3] = "Şıkkı";
        AddQuestion(question);
        question = new Question();
        question.id = "3";
        question.question = "Bu sorunun cevabı D şıkkı";
        question.answers[0] = "Bu a";
        question.answers[1] = "Bu b";
        question.answers[2] = "Bu c";
        question.answers[3] = "Bu d yani doğru cevap tıkla buna";
        AddQuestion(question);
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
}