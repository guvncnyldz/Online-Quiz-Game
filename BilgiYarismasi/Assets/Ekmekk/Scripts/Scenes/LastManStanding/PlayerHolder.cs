using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHolder : MonoBehaviour
{
    public Player player;
    private Character character;

    [SerializeField] private Transform eliminated;
    [SerializeField] private TextMeshProUGUI txt_answer, txt_nickname;
    [SerializeField] private Image background, race;
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Sprite[] races;
    public bool isFail;

    private void Awake()
    {
        character = GetComponentInChildren<Character>();
    }

    public void AnswerQuestion(int answer)
    {
        background.gameObject.SetActive(true);
        background.sprite = backgrounds[0];

        switch (answer)
        {
            case 0:
                txt_answer.text = "A";
                txt_answer.color = Color.yellow;
                break;
            case 1:
                txt_answer.text = "B";
                txt_answer.color = Color.yellow;
                break;
            case 2:
                txt_answer.text = "C";
                txt_answer.color = Color.yellow;
                break;
            case 3:
                txt_answer.text = "D";
                txt_answer.color = Color.yellow;
                break;
        }
    }

    public void Correct()
    {
        background.gameObject.SetActive(true);

        background.sprite = backgrounds[1];
        txt_answer.color = Color.green;

        character.charAnim.SetTrigger(CharAnimsTag.win);
    }

    public void Wrong()
    {
        background.gameObject.SetActive(true);

        background.sprite = backgrounds[2];
        txt_answer.color = Color.red;
        isFail = true;

        character.charAnim.SetTrigger(CharAnimsTag.lose);
    }

    public void NextQuestion()
    {
        txt_answer.text = "";

        if (!isFail)
        {
            background.gameObject.SetActive(false);
            character.charAnim.SetTrigger(CharAnimsTag.idle);
        }
        else
        {
            eliminated.gameObject.SetActive(true);
        }
    }

    public void SetPlayer(JArray response)
    {
        player = new Player();

        player.SetUser(response);

        txt_nickname.text = player.Username;
        txt_nickname.color = Color.white;
        race.sprite = races[player.Race];

        character.cosmetic.SetCosmetic(player.cosmeticData);
    }

    public void SetUser()
    {
        player = new Player();

        player.Username = User.GetInstance().Username;
        player.SetManuelProfileId(User.GetInstance().ProfileId);
        txt_nickname.text = player.Username;
        txt_nickname.color = Color.yellow;
        race.sprite = races[player.Race];

        character.cosmetic.SetCosmetic(User.GetInstance().cosmeticData);
    }

    public void Fall()
    {
        background.gameObject.SetActive(true);

        background.sprite = backgrounds[2];
        txt_answer.color = Color.red;
        isFail = true;
    }
}