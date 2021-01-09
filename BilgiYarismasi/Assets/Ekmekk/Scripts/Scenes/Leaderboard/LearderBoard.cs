using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LearderBoard : MonoBehaviour
{
    private const int LEADERCOUNTINONEPAGE = 2;
    [SerializeField] private GameObject leaderHolder;

    private List<JToken> leaderList;
    private List<GameObject> leaderHolders;

    public int totalPageCount = 0;
    public int currentPage = 0;

    private void Awake()
    {
        leaderHolders = new List<GameObject>();
        leaderList = new List<JToken>();
    }

    public void SetRank(JArray response)
    {
        if (leaderList.Count > 0)
        {
            leaderList.Clear();
        }

        foreach (JToken token in response)
        {
            leaderList.Add(token);
        }

        float leaderCount = leaderList.Count / (float) LEADERCOUNTINONEPAGE;

        if (leaderCount != 0)
            totalPageCount = Mathf.CeilToInt(leaderCount);
        else
            totalPageCount = 0;

        if (totalPageCount == 0)
            currentPage = 0;
        else
        {
            currentPage = 1;
        }

        SetBoard();
    }

    private void SetBoard()
    {
        if (leaderHolders.Count > 0)
        {
            foreach (GameObject holders in leaderHolders)
            {
                Destroy(holders);
            }

            leaderHolders.Clear();
        }

        if (totalPageCount <= 0)
            return;

        for (int i = (currentPage - 1) * LEADERCOUNTINONEPAGE; i < currentPage * LEADERCOUNTINONEPAGE; i++)
        {
            if (i >= leaderList.Count)
                break;

            GameObject temp = Instantiate(leaderHolder, transform);
            temp.GetComponent<LeaderHolder>().SetData(leaderList[i], i);
            leaderHolders.Add(temp);
        }
    }

    public void FirstPage()
    {
        if (currentPage > 1 && totalPageCount > 0)
        {
            currentPage = 1;
            SetBoard();
        }
    }

    public void NextPage()
    {
        if (currentPage < totalPageCount && totalPageCount > 0)
        {
            currentPage++;
            SetBoard();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 1 && totalPageCount > 0)
        {
            currentPage--;
            SetBoard();
        }
    }

    public void PlayerPage()
    {
        if (totalPageCount <= 0)
            return;

        int count = 1;

        foreach (JToken jToken in leaderList)
        {
            if (jToken["profile"][0]["_id"].ToString() == User.GetInstance().ProfileId)
            {
                break;
            }

            count++;
        }

        float value = count / (float) LEADERCOUNTINONEPAGE;
        currentPage = Mathf.CeilToInt(value);

        SetBoard();
    }
}