using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuTournament : MonoBehaviour
{
    [SerializeField] private GameObject btn_tournament;

    private void Awake()
    {
        GetTournament();
    }

    async void GetTournament()
    {
        var values = new Dictionary<string, string>
        {
            {"user_id", User.GetInstance().UserId}
        };

        JArray response = await HTTPApiUtil.Post(values, "tournament", "gettournamentinfo");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            if (error.errorCode == ErrorHandler.NotFound)
            {
                return;
            }
            else
            {
                SceneManager.LoadScene((int) Scenes.Fail);
                return;
            }
        }

        if (response.Count > 0)
        {
            btn_tournament.SetActive(true);
            TournamentData.SetTournament(response[0]);
        }
    }
}