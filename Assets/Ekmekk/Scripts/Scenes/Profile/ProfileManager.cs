using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private Button btn_statistic, btn_achievement, btn_editName, btn_editRace;
    [SerializeField] private Sprite[] races;
    [SerializeField] private TextMeshProUGUI txt_nickname;
    [SerializeField] private GameObject obj_statistic, obj_achievement;
    [SerializeField] private Sprite clickedButton, unClickedButton;
    [SerializeField] private Image img_race;

    private ProfileStatistic profileStatistic;

    private void Awake()
    {
        profileStatistic = FindObjectOfType<ProfileStatistic>();
        GetStatistic();

        btn_statistic.image.sprite = clickedButton;
        btn_achievement.image.sprite = unClickedButton;
        obj_achievement.SetActive(false);
        obj_statistic.SetActive(true);
        img_race.sprite = races[User.GetInstance().Race];
        txt_nickname.text = User.GetInstance().Username;

        btn_achievement.onClick.AddListener(() =>
        {
            btn_statistic.image.sprite = unClickedButton;
            btn_achievement.image.sprite = clickedButton;

            obj_achievement.SetActive(true);
            obj_statistic.SetActive(false);
        });

        btn_statistic.onClick.AddListener(() =>
        {
            btn_statistic.image.sprite = clickedButton;
            btn_achievement.image.sprite = unClickedButton;

            obj_achievement.SetActive(false);
            obj_statistic.SetActive(true);
        });

        btn_editRace.onClick.AddListener(() => EditRace());
        btn_editName.onClick.AddListener(() => EditName());
    }

    async void GetStatistic()
    {
        var values = new Dictionary<string, string>
        {
            {"profile_id", User.GetInstance().ProfileId},
        };

        JArray response = await HTTPApiUtil.Post(values, "score", "statistic");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
            return;
        }

        profileStatistic.Set(response);
    }
    void EditRace()
    {
        FindObjectOfType<IAP>().BuyConsumable("changeRace", () =>
        {
            User.GetInstance().Race = -1;
            SceneManager.LoadScene((int) Scenes.Opening);
        });
    }

    void EditName()
    {
        FindObjectOfType<ProfilePopUp>().AddListenerToButton(txt_nickname).SetAndShow();
    }
}