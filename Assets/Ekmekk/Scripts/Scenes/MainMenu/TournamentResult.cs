using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TournamentResult : MonoBehaviour
{
    private float disappearY;
    private RectTransform rectTransform;

    [SerializeField] private TextMeshProUGUI txt_money, txt_gold, txt_count;
    [SerializeField] private Image blackScreen;
    [SerializeField] private Button btn_getPrice;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        disappearY = rectTransform.anchoredPosition.y;
        blackScreen.enabled = false;
    }

    public async void CheckResult()
    {
        Debug.Log("kontrol");
        
        var values = new Dictionary<string, string>
        {
            {"user_id", User.GetInstance().UserId},
        };

        JArray response = await HTTPApiUtil.Post(values, "tournament", "checktournamentresult");

        Error error = ErrorHandler.Handle(response);

        if (error.isError && error.errorCode != ErrorHandler.NotFound)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
            return;
        }
        
        if(error.errorCode != ErrorHandler.NotFound)
        {
            int moneyAward = Convert.ToInt16(response[0]["award"]["money_award"]);
            int goldAward = Convert.ToInt16(response[0]["award"]["gold_award"]);
            int count = Convert.ToInt16(response[0]["count"]);

            txt_money.text = moneyAward.ToString();
            txt_gold.text = goldAward.ToString();
            
            if (count != -1)
            {
                txt_count.text = "Sıralaman: " + count + ".";
            }
            else
            {
                txt_count.text = Random.Range(5, 1000).ToString();
            }

            

            btn_getPrice.onClick.AddListener(() =>
            {
                Disappear(() =>
                {
                    btn_getPrice.enabled = false;
                  
                    User.GetInstance().Coin += goldAward;
                    User.GetInstance().Money += moneyAward;
                    
                    FindObjectOfType<UIMenuManager>().SetMoney();
                });
            });

            Appear(() => { });
        }
    }

    void Disappear(Action onComplete)
    {
        rectTransform.DOAnchorPosY(disappearY, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            blackScreen.enabled = false;
            onComplete?.Invoke();
        });
    }

    void Appear(Action onComplete)
    {
        rectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
        blackScreen.enabled = true;
    }
}