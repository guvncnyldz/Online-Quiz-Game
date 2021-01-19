using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private UIMenuManager uiMenuManager;
    private SecondPopUp secondPopUp;
    private PopUp popUp;

    private bool isPanelOpen;

    private void Start()
    {
        secondPopUp = FindObjectOfType<SecondPopUp>();
        popUp = FindObjectOfType<PopUp>();
        uiMenuManager = GetComponent<UIMenuManager>();
        FindObjectOfType<Character>().cosmetic.SetCosmetic(User.GetInstance().cosmeticData);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPanelOpen)
        {
            Logout();
        }
    }

    public void Tournament()
    {
        SceneManager.LoadScene((int) Scenes.Tournament);
    }

    public void Inventory()
    {
        SceneManager.LoadScene((int) Scenes.Inventory);
    }

    public void Profile()
    {
        SceneManager.LoadScene((int) Scenes.Profile);
    }

    public void Market()
    {
        SceneManager.LoadScene((int) Scenes.Market);
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene((int) Scenes.LeaderBoard);
    }

    public void Logout()
    {
        isPanelOpen = true;

        secondPopUp.ResetListenerFromSecondButton().ResetListenerFromButton()
            .AddListenerToButton(() => { isPanelOpen = false; }).AddListenerToSecondButton(() =>
            {
                LoginHttp.Logout();
                SceneManager.LoadScene((int) Scenes.Login);
            }).SetAndShow("Çıkış", "Çıkış yapmak istediğinize emin misiniz?", "Çık");
    }

    public void TrainingMode()
    {
        isPanelOpen = true;
        secondPopUp.ResetListenerFromSecondButton().ResetListenerFromButton().AddListenerToButton(() =>
            {
                isPanelOpen = false;
            })
            .AddListenerToSecondButton(() => { SceneManager.LoadScene((int) Scenes.TrainingGame); })
            .SetAndShow("Eğitim", "1 enerji harcanacak", "Oyna");
    }

    public void LastManStanding()
    {
        SceneManager.LoadScene((int) Scenes.LastManStanding);
    }

    public void Millionaire()
    {
        SceneManager.LoadScene((int) Scenes.Millionaire);
    }

    public void WordHunt()
    {
        SceneManager.LoadScene((int) Scenes.WordHunt);
    }

    public void ChangeGameTypesBottomMenu()
    {
        uiMenuManager.MoveBottom();
        uiMenuManager.MoveGameType();
    }
}