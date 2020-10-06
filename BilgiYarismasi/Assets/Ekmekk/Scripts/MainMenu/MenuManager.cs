using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button btn_play;

    private void Awake()
    {
        ModSelectPanel modSelectPanel = FindObjectOfType<ModSelectPanel>();
        btn_play.onClick.AddListener(() => { modSelectPanel.gameObject.SetActive(true); });
    }
}