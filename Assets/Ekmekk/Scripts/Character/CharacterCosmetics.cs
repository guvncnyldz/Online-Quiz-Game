using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCosmetics : MonoBehaviour
{
    [SerializeField] private Image head, body, handLeft, handRight, footLeft, footRight, eye, hair;

    [SerializeField] private ItemData itemData;

    public void SetCosmetic(CosmeticData cosmeticData)
    {
         GetSprite(CosmeticTypes.Head, cosmeticData.head,ref head);
         GetSprite(CosmeticTypes.Body, cosmeticData.body,ref body);
         GetSprite(CosmeticTypes.Hand, cosmeticData.handLeft,ref handLeft);
         GetSprite(CosmeticTypes.Hand, cosmeticData.handLeft,ref handRight);
         GetSprite(CosmeticTypes.Foot, cosmeticData.footLeft,ref footLeft);
         GetSprite(CosmeticTypes.Foot, cosmeticData.footLeft,ref footRight);
         GetSprite(CosmeticTypes.Eye, cosmeticData.eye,ref eye);
         GetSprite(CosmeticTypes.Hair, cosmeticData.hair,ref hair);
    }

    void GetSprite(CosmeticTypes type, string id, ref Image image)
    {
        foreach (Item item in itemData.items)
        {
            if (item.id == id)
            {
                image.sprite = item.sprite;
                return;
            }
        }

        foreach (Item item in itemData.items)
        {
            if (item.isDefault && item.cosmeticTypes == type)
            {
                image.sprite = item.sprite;
                return;
            }
        }
    }
}

public static class XX
{
}