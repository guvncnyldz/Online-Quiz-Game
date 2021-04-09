using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EarningData", menuName = "Datas/EarningData")]
public class EarningData : ScriptableObject
{
    [Header("WordHunt")] public int minCostPerWord;
    public int maxCostPerWord;
    [Header("TrainingManager")] public int minCostPerCorrectTraining;
    public int maxCostPerCorrectTraining;

    [Header("LastManStanding")] public int minCostPerCorrectLastMan;
    public int maxCostPerCorrectLastMan;
    public int minLastManCost;
    public int maxLastManCost;
}