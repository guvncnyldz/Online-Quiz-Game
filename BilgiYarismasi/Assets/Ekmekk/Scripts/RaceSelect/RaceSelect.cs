using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RaceSelect : MonoBehaviour
{
    [SerializeField] private Button btn_fire, btn_air, btn_earth, btn_water;

    private Vector3 startRotation;

    private RaceDetail raceDetail;

    private void Awake()
    {
        raceDetail = FindObjectOfType<RaceDetail>();

        startRotation = transform.rotation.eulerAngles;

        btn_air.onClick.AddListener(() => ChooseRace(btn_air, RacesIndex.Air));
        btn_fire.onClick.AddListener(() => ChooseRace(btn_fire, RacesIndex.Fire));
        btn_earth.onClick.AddListener(() => ChooseRace(btn_earth, RacesIndex.Earth));
        btn_water.onClick.AddListener(() => ChooseRace(btn_water, RacesIndex.Water));
        
        ChooseRace(btn_fire,RacesIndex.Fire);
    }

    void ChooseRace(Button selectedButton, RacesIndex selectedRace)
    {
        LockButtons(false);

        float rotateTargetZ = 0;
        string raceName = "";

        switch (selectedRace)
        {
            case RacesIndex.Fire:
                raceName = "ATEŞ";
                rotateTargetZ = 0;
                break;
            case RacesIndex.Air:
                raceName = "HAVA";
                rotateTargetZ = 90;
                break;
            case RacesIndex.Earth:
                raceName = "TOPRAK";
                rotateTargetZ = 180;
                break;
            case RacesIndex.Water:
                raceName = "SU";
                rotateTargetZ = -90;
                break;
        }

        raceDetail.SetDetail(raceName, Races.colorCodes[selectedRace], "");
        
        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotateTargetZ),
            160).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() => { LockButtons(true); });
    }

    void LockButtons(bool isUnlocked)
    {
        btn_air.enabled = isUnlocked;
        btn_earth.enabled = isUnlocked;
        btn_fire.enabled = isUnlocked;
        btn_water.enabled = isUnlocked;
    }
}