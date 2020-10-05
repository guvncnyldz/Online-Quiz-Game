using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrainingEndPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_score, txt_correct;
    [SerializeField] private Button btn_home, btn_restart;

    private void Start()
    {
        gameObject.SetActive(false);
        btn_home.onClick.AddListener(() => SceneManager.LoadScene((int) Scenes.Main));
        btn_restart.onClick.AddListener(() => SceneManager.LoadScene((int) Scenes.Game));
    }

    public void SetValues(int score, int correct)
    {
        txt_correct.text = correct.ToString();
        txt_score.text = score.ToString();
    }
}