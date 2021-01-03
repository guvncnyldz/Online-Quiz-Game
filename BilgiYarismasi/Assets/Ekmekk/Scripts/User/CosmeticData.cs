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

    public void SetCosmetic(JArray info)
    {
        head = info[0]["user"]["profile"]["cosmetic"]["head"].ToString();
        body = info[0]["user"]["profile"]["cosmetic"]["body"].ToString();
        handLeft = handRight = info[0]["user"]["profile"]["cosmetic"]["hand"].ToString();
        footLeft = footRight = info[0]["user"]["profile"]["cosmetic"]["foot"].ToString();
        hair = info[0]["user"]["profile"]["cosmetic"]["hair"].ToString();
        eye = info[0]["user"]["profile"]["cosmetic"]["eye"].ToString();
    }
}