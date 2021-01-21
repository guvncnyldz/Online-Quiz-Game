using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemButton : MonoBehaviour
{
    public CosmeticTypes type;
    public string sprite_name;
    public string name;

    public void Use()
    {
        switch (type)
        {
            case CosmeticTypes.Body:
            {
                if (User.GetInstance().cosmeticData.body != sprite_name)
                {
                    User.GetInstance().cosmeticData.body = sprite_name;
                }

                break;
            }
            case CosmeticTypes.Hand:
                if (User.GetInstance().cosmeticData.handLeft != sprite_name)
                {
                    User.GetInstance().cosmeticData.handLeft =
                        User.GetInstance().cosmeticData.handRight = sprite_name;
                }

                break;
            case CosmeticTypes.Hair:
                if (User.GetInstance().cosmeticData.hair != sprite_name)
                {
                    User.GetInstance().cosmeticData.hair = sprite_name;
                }

                break;
            case CosmeticTypes.Foot:
                if (User.GetInstance().cosmeticData.footLeft != sprite_name)
                {
                    User.GetInstance().cosmeticData.footLeft =
                        User.GetInstance().cosmeticData.footRight = sprite_name;
                }

                break;
            case CosmeticTypes.Eye:
                if (User.GetInstance().cosmeticData.eye != sprite_name)
                {
                    User.GetInstance().cosmeticData.eye = sprite_name;
                }

                break;
            case CosmeticTypes.Head:
                if (User.GetInstance().cosmeticData.head != sprite_name)
                {
                    User.GetInstance().cosmeticData.head = sprite_name;
                }

                break;
        }

        User.GetInstance().cosmeticData.SaveCosmetics();
    }

    public void Set(InventoryCosmetic inventoryCosmetic)
    {
        type = inventoryCosmetic.type;
        sprite_name = inventoryCosmetic.sprite_name;
        name = inventoryCosmetic.name;
        
        GetComponent<Image>().sprite =
            Resources.Load<Sprite>("Cosmetics/Character/" + type + "/" + sprite_name);
    }

    public void Set(string sprite_name, string name)
    {
        type = CosmeticTypes.Head;
        this.sprite_name = sprite_name;
        this.name = name;
        
        GetComponent<Image>().sprite =
            Resources.Load<Sprite>("Cosmetics/Character/" + type + "/" + sprite_name);
    }
}
