using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_score, txt_correct;
    [SerializeField] private Button btn_home, btn_restart;
    [SerializeField] private Image glow,blackScreen;

    private void Start()
    {
        gameObject.SetActive(false);
        blackScreen.enabled = false;
        btn_home.onClick.AddListener(() => SceneManager.LoadScene((int) Scenes.Main));
        btn_restart.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void SetValues(int score, int correct)
    {
        gameObject.SetActive(true);
        blackScreen.enabled = true;

        int currentCorrect = 0;
        int currentScore = 0;

        GetComponent<RectTransform>().DOAnchorPosY(0, 0.7f).OnComplete(() =>
        {
            DOTween.To(() => currentScore, x => currentScore = x, correct, 1).OnUpdate(() =>
            {
                txt_correct.text = currentScore.ToString();
            });

            DOTween.To(() => currentCorrect, x => currentCorrect = x, score, 1).OnUpdate(() =>
            {
                txt_score.text = currentCorrect.ToString();
            });

            glow.DOFade(1, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        });
    }
}