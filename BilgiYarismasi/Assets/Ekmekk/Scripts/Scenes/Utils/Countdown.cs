using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDown;

    public void StartCountDown(Action onCountdownEnd)
    {
        StartCoroutine(CountDown(onCountdownEnd));
    }

    IEnumerator CountDown(Action onCountdownEnd)
    {
        float currentTime = 3;
        countDown.gameObject.SetActive(true);

        while (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;
            countDown.fontSize = Mathf.Lerp(246.2f, 20.4f,
                Mathf.InverseLerp(Mathf.Floor(currentTime), Mathf.Ceil(currentTime), currentTime));
            countDown.text = Mathf.Ceil(currentTime).ToString();
            yield return null;
        }

        countDown.gameObject.SetActive(false);
        onCountdownEnd?.Invoke();
    }
}