using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPopup : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private Button exit, play, sound;
    [SerializeField] private Image blackScreen;

    private bool isOpen;

    private void Awake()
    {
        blackScreen.enabled = false;
        
        GetComponent<Button>().onClick.AddListener(() =>
        {
            PanelOpen();
        });
        
        exit.onClick.AddListener(() =>
        {
            Destroy(menu.gameObject);

            LastManManager lastManManager = FindObjectOfType<LastManManager>();
            if (lastManManager)
            {
                lastManManager.EndGame(0);
                return;
            }

            TrainingManager trainingManager = FindObjectOfType<TrainingManager>();

            if (trainingManager)
            {
                trainingManager.EndGame(0);
                return;
            }

            MillionareManager millionareManager = FindObjectOfType<MillionareManager>();

            if (millionareManager)
            {
                millionareManager.EndGame(1);
                return;
            }

            WordHuntManager wordHuntManager = FindObjectOfType<WordHuntManager>();
            wordHuntManager.EndGame(false);
            
        });
        
        play.onClick.AddListener(() =>
        {
            blackScreen.enabled = false;
            menu.SetActive(false);
            isOpen = false;
        });
        
        sound.onClick.AddListener(() =>
        {
        });
    }

    private void PanelOpen()
    {
        isOpen = !isOpen;
        blackScreen.enabled = isOpen;
        menu.SetActive(isOpen);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PanelOpen();
        }
    }

    private void OnDisable()
    {
        menu.SetActive(false);
        blackScreen.enabled = false;
        isOpen = false;
    }

    private void OnDestroy()
    {
        Destroy(menu);
    }
}