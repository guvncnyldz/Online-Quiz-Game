using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerPanel : MonoBehaviour
{
    const float APPEARY = -171;
    const float DISAPPEARY = 129;

    private PlayerHolder[] playerHolders;
    private RectTransform rectTransform;
    private LastManManager lastManManager;

    [SerializeField] private TextMeshProUGUI txt_questionCounter, txt_userCounter;

    private bool isGameStart;

    public int userCount;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        lastManManager = FindObjectOfType<LastManManager>();
    }

    public void SetPanel(string[] pid)
    {
        int user = Random.Range(0, pid.Length);
        playerHolders = GetComponentsInChildren<PlayerHolder>();

        int i = 0;

        foreach (PlayerHolder playerHolder in playerHolders)
        {
            if (i < pid.Length)
            {
                if (pid[i] == User.GetInstance().ProfileId)
                {
                    playerHolder.SetUser();
                }
                else
                {
                    PlayerHTTP.GetPlayerInfo(pid[i], playerHolder);
                }

                userCount++;
                i++;
            }
            else
            {
                playerHolder.gameObject.SetActive(false);
            }
        }

        isGameStart = true;
    }

    public void AnswerQuestion(int answer, string pid)
    {
        foreach (PlayerHolder playerHolder in playerHolders)
        {
            if (playerHolder.gameObject.activeSelf && playerHolder.player.ProfileId == pid)
            {
                playerHolder.AnswerQuestion(answer);
            }
        }
    }

    public void Correct(string pid)
    {
        foreach (PlayerHolder playerHolder in playerHolders)
        {
            if (playerHolder.gameObject.activeSelf && playerHolder.player.ProfileId == pid)
            {
                playerHolder.Correct();
            }
        }
    }

    public void Wrong(string pid)
    {
        foreach (PlayerHolder playerHolder in playerHolders)
        {
            if (playerHolder.gameObject.activeSelf && playerHolder.player.ProfileId == pid)
            {
                playerHolder.Wrong();
                userCount--;
            }
        }
    }

    public void NextQuestion()
    {
        foreach (PlayerHolder playerHolder in playerHolders)
        {
            if (playerHolder.gameObject.activeSelf && !playerHolder.isFail)
            {
                playerHolder.NextQuestion();
            }
        }
    }

    private void Update()
    {
        if (!isGameStart)
            return;

        txt_userCounter.text = "Kalan: " + userCount;
        txt_questionCounter.text = "Soru: " + lastManManager.GetCorrectCount();
    }

    public void AppearPanel(bool isAppear)
    {
        if (isAppear)
        {
            rectTransform.DOAnchorPosY(APPEARY, 0.25f).SetEase(Ease.Linear);
        }
        else
        {
            rectTransform.DOAnchorPosY(DISAPPEARY, 0.25f).SetEase(Ease.Linear);
        }
    }

    public void Fall(string pid)
    {
        foreach (PlayerHolder playerHolder in playerHolders)
        {
            if (playerHolder.gameObject.activeSelf && !playerHolder.isFail)
            {
                if (playerHolder.player.ProfileId == pid)
                {
                    playerHolder.Fall();
                    userCount--;
                }
            }
        }
    }
}