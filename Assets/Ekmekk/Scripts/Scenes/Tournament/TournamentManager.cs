using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TournamentManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt_date,txt_money,txt_gold, txt_goldPrice, txt_moneyPrice,txt_joinButton;
    [SerializeField] private GameObject lockPanel;
    [SerializeField] private Button btn_join;
    private TournamentBoard board;

    private void Awake()
    {
        txt_date.text = TournamentData.end_date;
        txt_money.text = TournamentData.money_award;
        txt_gold.text = TournamentData.gold_award;
        
        if (!TournamentData.joined)
        {
            LockPanel();   
        }
        else
        {
            lockPanel.gameObject.SetActive(false);
            board = FindObjectOfType<TournamentBoard>();
            GetRanks();
        }
    }
    void LockPanel()
    {
        txt_goldPrice.text = TournamentData.gold_price;
        txt_moneyPrice.text = TournamentData.money_price;

        int goldPrice = Convert.ToInt32(TournamentData.gold_price);
        int moneyPrice = Convert.ToInt32(TournamentData.money_price);

        if (User.GetInstance().Coin >= goldPrice && User.GetInstance().Money >= moneyPrice)
        {
            txt_joinButton.text = "Katıl";
            btn_join.onClick.AddListener(() =>
            {
                JoinTournament();
            });
        }
        else
        {
            txt_joinButton.text = "Yeterli Paran Yok";
        }
    }

    async void JoinTournament()
    {
        var values = new Dictionary<string, string>
        {
            {"user_id", User.GetInstance().UserId},
            {"tournament_id", TournamentData._id},
        };

        JArray response = await HTTPApiUtil.Post(values, "tournament", "jointournament");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
            return;
        }

        TournamentData.joined = true;
        SceneManager.LoadScene((int) Scenes.Tournament);
    }
    async void GetRanks()
    {
        var values = new Dictionary<string, string>
        {
            {"tournament_id", TournamentData._id},
        };

        JArray response = await HTTPApiUtil.Post(values, "tournament", "gettournamentstatistic");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
            return;
        }

        board.SetRank(response);
    }
}
