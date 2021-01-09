using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private UIMenuManager uiMenuManager;

    private void Start()
    {
        uiMenuManager = GetComponent<UIMenuManager>();
        FindObjectOfType<Character>().cosmetic.SetCosmetic(User.GetInstance().cosmeticData);
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene((int) Scenes.LeaderBoard);
    }

    public void Logout()
    {
        LoginHttp.Logout();
        SceneManager.LoadScene((int) Scenes.Login);
    }

    public void TrainingMode()
    {
        SceneManager.LoadScene((int) Scenes.TrainingGame);
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