using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCosmetics : MonoBehaviour
{
    [SerializeField] private Image head, body, handLeft, handRight, footLeft, footRight, eye, hair;

    public void SetCosmetic(CosmeticData cosmeticData)
    {
        Sprite headSprite = Resources.Load<Sprite>("Cosmetics/Character/Head/" + cosmeticData.head);
        if (headSprite)
            head.sprite = headSprite;

        Sprite bodySprite = Resources.Load<Sprite>("Cosmetics/Character/Body/" + cosmeticData.body);

        if (bodySprite)
            body.sprite = bodySprite;

        Sprite handSprite = Resources.Load<Sprite>("Cosmetics/Character/Hand/" + cosmeticData.handLeft);

        if (handSprite)
            handLeft.sprite = handRight.sprite = handSprite;

        Sprite footSprite = Resources.Load<Sprite>("Cosmetics/Character/Foot/" + cosmeticData.footLeft);

        if (footSprite)
            footLeft.sprite = footRight.sprite = footSprite;

        Sprite eyeSprite = Resources.Load<Sprite>("Cosmetics/Character/Eye/" + cosmeticData.eye);

        if (eyeSprite)
            eye.sprite = eyeSprite;

        Sprite hairSprite = Resources.Load<Sprite>("Cosmetics/Character/Hair/" + cosmeticData.hair);

        if (hairSprite)
            hair.sprite = hairSprite;
    }
}