using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuChanger : MonoBehaviour
{

    [SerializeField] private Sprite[] backgrounds;

    [SerializeField]
    private Image background;


    public void Awake()
    {
        background.sprite = backgrounds[User.GetInstance().Race];
    }
}
