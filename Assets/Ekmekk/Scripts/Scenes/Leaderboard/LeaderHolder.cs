using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderHolder : MonoBehaviour
{
    private Player player;

    [SerializeField] private Sprite[] ranks, races;
    [SerializeField] private Image img_rank, img_race;
    [SerializeField] private Character character;
    [SerializeField] private TextMeshProUGUI txt_name, txt_score, txt_rank;

    public void SetData(JToken info, JToken profile, int index)
    {
        player = new Player();

        player.SetUser(profile);

        if (player.ProfileId == User.GetInstance().ProfileId)
        {
            txt_name.color = Color.yellow;
        }

        character.cosmetic.SetCosmetic(player.cosmeticData);
        txt_name.text = player.Username;
        if (player.Race == -1)
        {
            img_race.gameObject.SetActive(false);
        }
        else
        {
            img_race.sprite = races[player.Race];
        }

        txt_rank.text = (index + 1).ToString();
        txt_score.text = info["true_answer"].ToString();

        switch (index)
        {
            case 0:
                img_rank.sprite = ranks[index];
                break;
            case 1:
                img_rank.sprite = ranks[index];
                break;
            case 2:
                img_rank.sprite = ranks[index];
                break;
            default:
                img_rank.sprite = ranks[3];
                break;
        }
    }
    
    public void SetData(int index)
    {
        player = new Player();
        player.SetUser(User.GetInstance().ProfileId,User.GetInstance().Username,User.GetInstance().Race,User.GetInstance().cosmeticData);

        if (player.ProfileId == User.GetInstance().ProfileId)
        {
            txt_name.color = Color.yellow;
        }

        character.cosmetic.SetCosmetic(player.cosmeticData);
        txt_name.text = player.Username;
        if (player.Race == -1)
        {
            img_race.gameObject.SetActive(false);
        }
        else
        {
            img_race.sprite = races[player.Race];
        }

        txt_rank.text = (index + 1).ToString();
        txt_score.text = 0.ToString();

        switch (index)
        {
            case 0:
                img_rank.sprite = ranks[index];
                break;
            case 1:
                img_rank.sprite = ranks[index];
                break;
            case 2:
                img_rank.sprite = ranks[index];
                break;
            default:
                break;
        }
    }
}