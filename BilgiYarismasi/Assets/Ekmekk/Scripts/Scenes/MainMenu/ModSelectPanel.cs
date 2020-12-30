using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModSelectPanel : MonoBehaviour
{
    [SerializeField] private Button btn_training, btn_wordhunt, btn_close;

    private void Start()
    {
        gameObject.SetActive(false);
        btn_training.onClick.AddListener(() => SceneManager.LoadScene((int) Scenes.TrainingGame));
        btn_wordhunt.onClick.AddListener(() => SceneManager.LoadScene((int) Scenes.WordHunt));
        btn_close.onClick.AddListener(() => gameObject.SetActive(false));
    }
}