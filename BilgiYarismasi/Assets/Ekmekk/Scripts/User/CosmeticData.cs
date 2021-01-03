using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticData : MonoBehaviour
{
    public string head, body, handLeft, handRight, footLeft, footRight, eye, hair;

    public void SaveCosmetics(CosmeticData cosmeticData)
    {
        head = cosmeticData.head;
        body = cosmeticData.body;
        handLeft = cosmeticData.handLeft;
        handRight = cosmeticData.handRight;
        footLeft = cosmeticData.footLeft;
        footRight = cosmeticData.footRight;
        eye = cosmeticData.eye;
        hair = cosmeticData.hair;
        
        Debug.Log("Data base güncellendi");
    }
}
