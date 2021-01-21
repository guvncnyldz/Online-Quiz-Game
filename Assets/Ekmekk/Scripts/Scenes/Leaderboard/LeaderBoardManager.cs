using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] private Button daily, weekly, monthly, yearly;
    [SerializeField] private Button training, lastman, millionaire, wordHunt;
    [SerializeField] private Button generalRace, currentRace;
    [SerializeField] private Button nextPage, previousPage, firstPage, playerPage;
    [SerializeField] private Sprite[] races;
    [SerializeField] private TextMeshProUGUI txt_pageCounter;

    [SerializeField] [CanBeNull]
    private Sprite img_longButton, img_shortButton, img_choosenLongButton, img_choosenShortButton;

    private string choosenDate, choosenRace, choosenMode;

    private LearderBoard board;

    private void Awake()
    {
        board = FindObjectOfType<LearderBoard>();

        currentRace.transform.GetChild(0).GetComponent<Image>().sprite = races[User.GetInstance().Race];

        nextPage.onClick.AddListener(() =>
        {
            board.NextPage();
            SetPageCount();
        });
        previousPage.onClick.AddListener(() =>
        {
            board.PreviousPage();
            SetPageCount();
        });
        firstPage.onClick.AddListener(() =>
        {
            board.FirstPage();
            SetPageCount();
        });
        playerPage.onClick.AddListener(() =>
        {
            board.PlayerPage();
            SetPageCount();
        });

        daily.onClick.AddListener(() =>
        {
            ChooseDate();
            choosenDate = "daily";
            daily.image.sprite = img_choosenLongButton;
            GetRanks();
        });
        weekly.onClick.AddListener(() =>
        {
            ChooseDate();
            choosenDate = "weekly";
            weekly.image.sprite = img_choosenLongButton;
            GetRanks();
        });
        monthly.onClick.AddListener(() =>
        {
            ChooseDate();
            choosenDate = "monthly";
            monthly.image.sprite = img_choosenLongButton;
            GetRanks();
        });
        yearly.onClick.AddListener(() =>
        {
            ChooseDate();
            choosenDate = "yearly";
            yearly.image.sprite = img_choosenLongButton;
            GetRanks();
        });
        training.onClick.AddListener(() =>
        {
            ChooseMod();
            choosenMode = ((int) GameMods.training).ToString();
            training.image.sprite = img_choosenLongButton;
            GetRanks();
        });
        lastman.onClick.AddListener(() =>
        {
            ChooseMod();
            choosenMode = ((int) GameMods.lastman).ToString();
            lastman.image.sprite = img_choosenLongButton;
            GetRanks();
        });
        millionaire.onClick.AddListener(() =>
        {
            ChooseMod();
            choosenMode = ((int) GameMods.millionaire).ToString();
            millionaire.image.sprite = img_choosenLongButton;
            GetRanks();
        });
        wordHunt.onClick.AddListener(() =>
        {
            ChooseMod();
            choosenMode = ((int) GameMods.wordHunt).ToString();
            wordHunt.image.sprite = img_choosenLongButton;
            GetRanks();
        });
        generalRace.onClick.AddListener(() =>
        {
            ChooseRace();
            choosenRace = "-1";
            generalRace.image.sprite = img_choosenShortButton;
            GetRanks();
        });
        currentRace.onClick.AddListener(() =>
        {
            ChooseRace();
            choosenRace = User.GetInstance().Race.ToString();
            currentRace.image.sprite = img_choosenShortButton;
            GetRanks();
        });
    }

    private void Start()
    {
        choosenDate = "daily";
        daily.image.sprite = img_choosenLongButton;
        choosenMode = ((int) GameMods.training).ToString();
        training.image.sprite = img_choosenLongButton;
        choosenRace = "-1";
        generalRace.image.sprite = img_choosenShortButton;
        GetRanks();
    }

    void ChooseDate()
    {
        daily.image.sprite = img_longButton;
        weekly.image.sprite = img_longButton;
        monthly.image.sprite = img_longButton;
        yearly.image.sprite = img_longButton;
    }

    void ChooseMod()
    {
        training.image.sprite = img_longButton;
        wordHunt.image.sprite = img_longButton;
        lastman.image.sprite = img_longButton;
        millionaire.image.sprite = img_longButton;
    }

    void ChooseRace()
    {
        generalRace.image.sprite = img_shortButton;
        currentRace.image.sprite = img_shortButton;
    }

    async void GetRanks()
    {
        var values = new Dictionary<string, string>
        {
            {"mod_id", choosenMode},
            {"race", choosenRace},
            {"type", choosenDate},
        };

        JArray response = await HTTPApiUtil.Post(values, "score", "ranking");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
            return;
        }

        board.SetRank(response);
        SetPageCount();
    }

    void SetPageCount()
    {
        txt_pageCounter.text = board.currentPage + "/" + board.totalPageCount;
    }
}