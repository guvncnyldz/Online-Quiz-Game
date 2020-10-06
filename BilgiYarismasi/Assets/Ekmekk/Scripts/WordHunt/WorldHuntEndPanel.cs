using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldHuntEndPanel : MonoBehaviour
{
    [SerializeField] private Button btn_home, btn_restart;

    private void Start()
    {
        gameObject.SetActive(false);
        btn_home.onClick.AddListener(() => SceneManager.LoadScene((int) Scenes.Main));
        btn_restart.onClick.AddListener(() => SceneManager.LoadScene((int) Scenes.WordHunt));
    }
}