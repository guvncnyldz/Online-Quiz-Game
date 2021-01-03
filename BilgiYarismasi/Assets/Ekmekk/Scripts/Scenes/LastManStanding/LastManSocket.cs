using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

public class LastManSocket : MonoBehaviour
{
    private const string ROUTE = "lastman";
    private PlayerPanel playerPanel;
    private LastManManager lastManManager;

    private bool isConnected;

    private string totalCount;
    [SerializeField] private Text loadingText;

    public void Awake()
    {
        playerPanel = FindObjectOfType<PlayerPanel>();
        lastManManager = FindObjectOfType<LastManManager>();

        SocketUtil.Connect();
        SocketUtil.ws.OnMessage += WsOnOnMessage;
        SocketUtil.ws.OnError += Error;

        if (SocketUtil.ws.IsAlive)
        {
            WsOnOnOpen(null, null);
        }
        else
        {
            SocketUtil.ws.OnOpen += WsOnOnOpen;
        }

        loadingText.text = "Bağlanıyor...";
    }


    private void WsOnOnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log(e.Data);
        JObject data = JObject.Parse(e.Data);

        if (data["route"].ToString() == ROUTE)
        {
            switch (data["method"].ToString())
            {
                case "connected":
                    Connected(data);
                    break;
                case "setcount":
                    SetCount(data);
                    break;
                case "start":
                    StartGame(data);
                    break;
                case "setquestion":
                    SetQuestion(data);
                    break;
                case "answer":
                    Answer(data);
                    break;
                case "result":
                    Result(data);
                    break;
                case "fall":
                    Fall(data);
                    break;
                default:
                    Debug.Log(e.Data);
                    Error(null, null);
                    break;
            }
        }
    }

    private void Connected(JObject data)
    {
        SocketUtil.ws.OnClose += WsOnOnClose;
        isConnected = true;
        totalCount = data["total_count"].ToString();

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            loadingText.text = data["player_count"] + "/" + totalCount;
        });
    }

    private void SetCount(JObject data)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            loadingText.text = data["player_count"] + "/" + totalCount;
        });
    }

    private void SetQuestion(JObject data)
    {
        Question question = new Question();
        question.Set(data);

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            playerPanel.NextQuestion();
            lastManManager.SetQuestion(question);
        });
    }

    private void StartGame(JObject data)
    {
        int userCount = Convert.ToInt16(data["player_count"].ToString());
        string[] userIds = new string[userCount];

        for (int i = 0; i < userCount; i++)
        {
            userIds[i] = data["pids"][i].ToString();
        }

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            playerPanel.SetPanel(userIds);
            loadingText.transform.parent.gameObject.SetActive(false);
        });
    }

    private void Answer(JObject data)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            playerPanel.AnswerQuestion(Convert.ToInt16(data["answer"].ToString()), data["pid"].ToString());
        });
    }

    private void Result(JObject data)
    {
        int playerCount = Convert.ToInt16(data["player_count"].ToString());

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {

            for (int i = 0; i < playerCount; i++)
            {
                if (data["results"][i]["result"].ToString().Equals("0"))
                {
                    playerPanel.Wrong(data["results"][i]["pid"].ToString());
                }
                else
                {
                    playerPanel.Correct(data["results"][i]["pid"].ToString());
                }

                if (i == playerCount - 1)
                {
                    lastManManager.Result(Convert.ToInt16(data["correct_answer"].ToString()));
                }
            }
        });
    }

    private void Fall(JObject data)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => { playerPanel.Fall(data["pid"].ToString()); });
    }

    private void WsOnOnClose(object sender, CloseEventArgs e)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.LoadScene((int) Scenes.Fail));
    }

    private void Error(object sender, ErrorEventArgs e)
    {
        Debug.Log(e.Exception);
        Debug.Log(e.Message);
        UnityMainThreadDispatcher.Instance().Enqueue(() => SceneManager.LoadScene((int) Scenes.Fail));
    }

    private void WsOnOnOpen(object sender, EventArgs e)
    {
        JObject jObject = new JObject();
        jObject.Add("route", ROUTE);
        jObject.Add("method", "joinlastmanroom");
        jObject.Add("pid", User.GetInstance().ProfileId);
        SocketUtil.ws.Send(jObject.ToString());
    }

    private void OnDestroy()
    {
        SocketUtil.ws.OnClose -= WsOnOnClose;
        SocketUtil.ws.OnOpen -= WsOnOnOpen;
        SocketUtil.ws.OnMessage -= WsOnOnMessage;
        SocketUtil.ws.OnError -= Error;

        SocketUtil.ws.Close();
    }
}