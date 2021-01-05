using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPopup : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private Button exit, play, sound;

    private bool isOpen;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            isOpen = !isOpen;
            menu.SetActive(isOpen);
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
            wordHuntManager.EndGame();
            
        });
        
        play.onClick.AddListener(() =>
        {
            menu.SetActive(false);
            isOpen = false;
        });
        
        sound.onClick.AddListener(() =>
        {
        });
    }
    private void OnDisable()
    {
        menu.SetActive(false);
        isOpen = false;
    }

    private void OnDestroy()
    {
        Destroy(menu);
    }
}