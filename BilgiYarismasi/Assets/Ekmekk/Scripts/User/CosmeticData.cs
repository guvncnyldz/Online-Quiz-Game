using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CosmeticData
{
    public string head, body, handLeft, handRight, footLeft, footRight, eye, hair;

    public async void SaveCosmetics(CosmeticData cosmeticData)
    {
        head = cosmeticData.head;
        body = cosmeticData.body;
        handLeft = cosmeticData.handLeft;
        handRight = cosmeticData.handRight;
        footLeft = cosmeticData.footLeft;
        footRight = cosmeticData.footRight;
        eye = cosmeticData.eye;
        hair = cosmeticData.hair;

        var values = new Dictionary<string, string>
        {
            //User id ile daha güvenli olur. Şimdilik durabilir
            {"pid", User.GetInstance().ProfileId},
            {"body", body},
            {"head", head},
            {"hand", handLeft},
            {"foot", footLeft},
            {"eye", eye},
            {"hair", hair},
        };

        JArray response = await HTTPApiUtil.Put(values, "inventory", "updatecosmetic");

        Error error = ErrorHandler.Handle(response);

        if (error.isError)
        {
            SceneManager.LoadScene((int) Scenes.Fail);
        }
    }

    public void SetCosmetic(JToken info)
    {
        head = info["head"].ToString();
        body = info["body"].ToString();
        handLeft = handRight = info["hand"].ToString();
        footLeft = footRight = info["foot"].ToString();
        hair = info["hair"].ToString();
        eye = info["eye"].ToString();
    }
}