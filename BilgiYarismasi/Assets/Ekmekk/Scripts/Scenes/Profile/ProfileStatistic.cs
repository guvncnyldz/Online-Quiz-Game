using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;

public class ProfileStatistic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI trainingCorrect,
        trainingCoin,
        lastManCorrect,
        lastManCoin,
        lastManWin,
        wordHuntCorrect,
        wordHuntCoin,
        wordHuntWin,
        millionaireCorrect,
        millionaireCoin,
        millionaireWin;

    public void Set(JToken statistics)
    {
        foreach (JToken statistic in statistics)
        {
            switch (Convert.ToInt16(statistic["_id"].ToString()))
            {
                case (int) GameMods.training:
                    trainingCoin.text = statistic["earn"].ToString();
                    trainingCorrect.text = statistic["true_answer"].ToString();
                    break;
                case (int) GameMods.lastman:
                    lastManCoin.text = statistic["earn"].ToString();
                    lastManCorrect.text = statistic["true_answer"].ToString();
                    lastManWin.text = statistic["win"].ToString();
                    break;
                case (int) GameMods.millionaire:
                    millionaireCoin.text = statistic["earn"].ToString();
                    millionaireCorrect.text = statistic["true_answer"].ToString();
                    millionaireWin.text = statistic["win"].ToString();
                    break;
                case (int) GameMods.wordHunt:
                    wordHuntCoin.text = statistic["earn"].ToString();
                    wordHuntCorrect.text = statistic["true_answer"].ToString();
                    wordHuntWin.text = statistic["win"].ToString();
                    break;
            }
        }
    }
}