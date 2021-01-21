using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using AlmostEngine.Screenshot;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class TournamentData
{
    public static string _id;
    public static string end_date;
    public static string money_award;
    public static string gold_award;
    public static string gold_price;
    public static string money_price;
    public static bool joined;

    public static void SetTournament(JToken token)
    {
        _id = token["_id"].ToString();
        end_date = DateTime.Parse(token["end_date"].ToString()).ToString("dd/MM/yyyy");
        money_award = token["money_award"].ToString();
        gold_award = token["gold_award"].ToString();
        money_price = token["money_price"].ToString();
        gold_price = token["gold_price"].ToString();
        joined = token["joined"].ToString() == "True";
    }
}