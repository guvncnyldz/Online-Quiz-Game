﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private const float FIRESTARTX = 198.8f;
    private const float FIREENDX = -187f;

    [SerializeField] private Image img_time;
    [SerializeField] private Image img_fire;
    [SerializeField] private float startTime;

    private float lastTime;
    private float currentTime;

    private Animator fireAnimator;
    private Tween fireburnout;
    private Coroutine countdown;

    public Action OnTimesUp;

    private void Awake()
    {
        fireAnimator = img_fire.GetComponent<Animator>();
        lastTime = startTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            StartCountdown();
        if (Input.GetKeyDown(KeyCode.S))
            StopCountdown();
        if (Input.GetKeyDown(KeyCode.D))
            RestartCountdown();
    }

    public void StartCountdown()
    {
        currentTime = lastTime;
        img_fire.gameObject.SetActive(true);
        fireAnimator.enabled = true;

        if (fireburnout != null && fireburnout.IsActive())
            fireburnout.Kill();

        countdown = StartCoroutine(Countdown());
    }

    public void StopCountdown()
    {
        if (countdown != null)
            StopCoroutine(countdown);
        
        fireAnimator.enabled = false;
        lastTime = currentTime;
        Color color = img_fire.color;
        color.a = 0;
        fireburnout = img_fire.DOColor(color, 0.5f).OnComplete(() =>
            img_fire.gameObject.SetActive(false));
    }

    public void RestartCountdown()
    {
        lastTime = startTime;
        img_time.fillAmount = 1;
        img_fire.rectTransform.anchoredPosition = new Vector2(FIRESTARTX, 0);
    }

    void TimesUp()
    {
        Debug.Log("Süre bitti");
        OnTimesUp?.Invoke();

        fireAnimator.enabled = false;
        Color color = img_fire.color;
        color.a = 0;
        fireburnout = img_fire.DOColor(color, 0.5f).OnComplete(() =>
            img_fire.gameObject.SetActive(false));
    }

    IEnumerator Countdown()
    {
        while (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;

            img_time.fillAmount = Mathf.InverseLerp(0, startTime, currentTime);
            img_fire.rectTransform.anchoredPosition =
                new Vector2(Mathf.Lerp(FIREENDX, FIRESTARTX, img_time.fillAmount), 0);

            yield return null;
        }

        TimesUp();
    }
}