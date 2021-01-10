using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class TournamentBoard : MonoBehaviour
{
    private const int LEADERCOUNTINONEPAGE = 7;
    
    [SerializeField] private GameObject leaderHolder;
    [SerializeField] private GameObject myBoard;
    
    private List<JToken> leaderList;
    private List<GameObject> leaderHolders;
    private void Awake()
    {
        leaderHolders = new List<GameObject>();
        leaderList = new List<JToken>();
    }

    public void SetRank(JArray response)
    {
        foreach (JToken token in response)
        {
            leaderList.Add(token);
        }

        SetBoard();
    }

    private void SetBoard()
    {
        for (int i = 0; i < LEADERCOUNTINONEPAGE; i++)
        {
            if (i >= leaderList.Count)
                break;

            GameObject temp = Instantiate(leaderHolder, transform);
            temp.GetComponent<LeaderHolder>().SetData(leaderList[i],leaderList[i]["profile"], i);
            leaderHolders.Add(temp);
        }

        foreach (JToken token in leaderList)
        {
            if (token["profile"]["_id"].ToString() == User.GetInstance().ProfileId)
            {
                GameObject temp = Instantiate(leaderHolder, myBoard.transform);
                temp.GetComponent<LeaderHolder>().SetData(token, token["profile"],leaderList.IndexOf(token));
                leaderHolders.Add(temp);
            }
        }
    }
}
