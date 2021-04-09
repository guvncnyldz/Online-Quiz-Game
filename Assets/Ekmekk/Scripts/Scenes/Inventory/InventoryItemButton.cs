using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryItemButton : MonoBehaviour
{
    public CosmeticTypes type;
    [FormerlySerializedAs("sprite_name")] public string sprite_id;
    public string name;

    public void Use()
    {
        switch (type)
        {
            case CosmeticTypes.Body:
            {
                if (User.GetInstance().cosmeticData.body != sprite_id)
                {
                    User.GetInstance().cosmeticData.body = sprite_id;
                }

                break;
            }
            case CosmeticTypes.Hand:
                if (User.GetInstance().cosmeticData.handLeft != sprite_id)
                {
                    User.GetInstance().cosmeticData.handLeft =
                        User.GetInstance().cosmeticData.handRight = sprite_id;
                }

                break;
            case CosmeticTypes.Hair:
                if (User.GetInstance().cosmeticData.hair != sprite_id)
                {
                    User.GetInstance().cosmeticData.hair = sprite_id;
                }

                break;
            case CosmeticTypes.Foot:
                if (User.GetInstance().cosmeticData.footLeft != sprite_id)
                {
                    User.GetInstance().cosmeticData.footLeft =
                        User.GetInstance().cosmeticData.footRight = sprite_id;
                }

                break;
            case CosmeticTypes.Eye:
                if (User.GetInstance().cosmeticData.eye != sprite_id)
                {
                    User.GetInstance().cosmeticData.eye = sprite_id;
                }

                break;
            case CosmeticTypes.Head:
                if (User.GetInstance().cosmeticData.head != sprite_id)
                {
                    User.GetInstance().cosmeticData.head = sprite_id;
                }

                break;
        }

        User.GetInstance().cosmeticData.SaveCosmetics();
    }

    public void Set(Item item)
    {
        type = item.cosmeticTypes;
        sprite_id = item.id;
        name = item.name;

        GetComponent<Image>().sprite = item.sprite;
    }
}
