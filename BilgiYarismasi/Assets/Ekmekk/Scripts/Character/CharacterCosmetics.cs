using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCosmetics : MonoBehaviour
{
    [SerializeField] private Image head, body, handLeft, handRight, footLeft, footRight, eye, hair;

    public void SetCosmetic(CosmeticData cosmeticData)
    {
        head.sprite = Resources.Load<Sprite>(cosmeticData.head);
        body.sprite = Resources.Load<Sprite>(cosmeticData.body);
        handLeft.sprite = Resources.Load<Sprite>(cosmeticData.handLeft);
        handRight.sprite = Resources.Load<Sprite>(cosmeticData.handRight);
        footLeft.sprite = Resources.Load<Sprite>(cosmeticData.footLeft);
        footRight.sprite = Resources.Load<Sprite>(cosmeticData.footRight);
        eye.sprite = Resources.Load<Sprite>(cosmeticData.eye);
        hair.sprite = Resources.Load<Sprite>(cosmeticData.hair);
    }
}