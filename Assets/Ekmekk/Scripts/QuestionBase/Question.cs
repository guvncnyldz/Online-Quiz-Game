using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class Question
{
    public string id;
    public string question;
    public string[] answers;
    public int correct;

    public Question()
    {
        answers = new string[4];
    }

    public void Set(JToken jQuestion)
    {
        id = jQuestion["_id"].ToString();
        question = jQuestion["question"].ToString();
        answers[0] = jQuestion["answerA"].ToString();
        answers[1] = jQuestion["answerB"].ToString();
        answers[2] = jQuestion["answerC"].ToString();
        answers[3] = jQuestion["answerD"].ToString();

        if (jQuestion["correct_answer"] != null)
            correct = Convert.ToInt16(jQuestion["correct_answer"].ToString());
    }
}