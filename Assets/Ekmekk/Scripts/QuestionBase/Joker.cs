﻿using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Joker : MonoBehaviour
{
    private RectTransform rectTransform;

    [SerializeField] private Transform projectile;
    [SerializeField] private Button btn_correct, btn_bomb, btn_pass;
    [SerializeField] private GameObject[] spells;

    private TextMeshProUGUI txt_correct, txt_bomb, txt_pass;
    private bool absoluteLock;

    private void Awake()
    {
        LockButton(true);
        rectTransform = GetComponent<RectTransform>();

        txt_correct = btn_correct.GetComponentInChildren<TextMeshProUGUI>();
        txt_bomb = btn_bomb.GetComponentInChildren<TextMeshProUGUI>();
        txt_pass = btn_pass.GetComponentInChildren<TextMeshProUGUI>();

        UpdateText();

        btn_bomb.onClick.AddListener(Bomb);
        btn_pass.onClick.AddListener(Pass);
        btn_correct.onClick.AddListener(Correct);
    }

    void UpdateText()
    {
        txt_correct.text = "x" + User.GetInstance().jokerData.Correct;
        txt_bomb.text = "x" + User.GetInstance().jokerData.Bomb;
        txt_pass.text = "x" + User.GetInstance().jokerData.Pass;
    }

    void Bomb()
    {
        btn_bomb.enabled = false;

        if (User.GetInstance().jokerData.Bomb > 0)
        {
            User.GetInstance().jokerData.Bomb--;
            List<int> answers = new List<int>() {0, 1, 2, 3};
            int correct = Convert.ToInt16(FindObjectOfType<QuestionBase>().currentQuestion.correct);
            answers.Remove(correct);

            AnswerController answerController = FindObjectOfType<AnswerController>();
            int answer1 = answers[Random.Range(0, 3)];
            answerController.answers[answer1].GetComponent<Button>().enabled = false;
            CreateSpell().Shot(answerController.answers[answer1].img_choice.transform,
                () => answerController.JokerEffect(answer1));
            answers.Remove(answer1);
            int answer2 = answers[Random.Range(0, 2)];
            answerController.answers[answer2].GetComponent<Button>().enabled = false;
            CreateSpell().Shot(answerController.answers[answer2].img_choice.transform,
                () => answerController.JokerEffect(answer2));
        }

        UpdateText();
    }

    void Pass()
    {
        btn_pass.enabled = false;

        if (User.GetInstance().jokerData.Pass > 0)
        {
            User.GetInstance().jokerData.Pass--;
            QuestionPanel questionPanel = FindObjectOfType<QuestionPanel>();
            CreateSpell().Shot(questionPanel.transform, () => { questionPanel.JokerEffect(); });

            FindObjectOfType<QuestionBase>().Pass();
        }

        UpdateText();
    }

    void Correct()
    {
        btn_correct.enabled = false;
        if (User.GetInstance().jokerData.Correct > 0)
        {
            User.GetInstance().jokerData.Correct--;

            FindObjectOfType<Timer>().StopCountdown();
            FindObjectOfType<MenuPopup>().gameObject.SetActive(false);
            AnswerController answerController = FindObjectOfType<AnswerController>();

            int correct = Convert.ToInt16(FindObjectOfType<QuestionBase>().currentQuestion.correct);
            CreateSpell().Shot(answerController.answers[correct].img_choice.transform,
                () => FindObjectOfType<QuestionBase>().CheckAnswer(correct));
        }

        UpdateText();
    }

    public void Fall()
    {
        int[] rotateDir = {-1, 1};
        int dir = rotateDir[Random.Range(0, 2)];

        Vector3 eulerAngles = transform.eulerAngles;

        Vector3 posTarget = new Vector3(0, -800, 0);
        rectTransform.DOAnchorPos(posTarget, 0.5f).SetEase(Ease.InCubic);

        Vector3 rotateTarget = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z + 75 * dir);
        rectTransform.DORotate(rotateTarget, 0.5f)
            .SetEase(Ease.InCubic).OnComplete(() => { transform.eulerAngles = new Vector3(0, 0, 0); });
    }

    public void LockButton(bool isLocked)
    {
        if (absoluteLock)
            return;

        btn_bomb.enabled = !isLocked;
        btn_correct.enabled = !isLocked;
        btn_pass.enabled = !isLocked;
    }

    Spell CreateSpell()
    {
        Spell spell = Instantiate(spells[User.GetInstance().Race], projectile).GetComponent<Spell>();
        spell.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        return spell;
    }

    public void AbsoluteLockButton()
    {
        absoluteLock = true;

        btn_bomb.enabled = false;
        btn_correct.enabled = false;
        btn_pass.enabled = false;
    }
}