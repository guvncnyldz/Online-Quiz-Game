using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RaceSelector : MonoBehaviour
{
    [SerializeField] private Button btn_fire, btn_air, btn_earth, btn_water;

    private RacePanel racePanel;
    public RacesIndex selectedRace;
    public Button selectedButton;

    private void Awake()
    {
        racePanel = FindObjectOfType<RacePanel>();

        btn_air.onClick.AddListener(() => ChooseRace(btn_air, RacesIndex.Air));
        btn_fire.onClick.AddListener(() => ChooseRace(btn_fire, RacesIndex.Fire));
        btn_earth.onClick.AddListener(() => ChooseRace(btn_earth, RacesIndex.Earth));
        btn_water.onClick.AddListener(() => ChooseRace(btn_water, RacesIndex.Water));

        selectedRace = RacesIndex.Fire;
        selectedButton = btn_fire;
        racePanel.SetDetailDirect(Races.races[selectedRace].name, Races.races[selectedRace].color,
            Races.races[selectedRace].detail);
    }

    void ChooseRace(Button selectedButton, RacesIndex selectedRace)
    {
        if (this.selectedRace == selectedRace)
            return;

        LockButtons(false);

        float rotateTargetZ = 0;

        switch (selectedRace)
        {
            case RacesIndex.Fire:
                rotateTargetZ = 0;
                break;
            case RacesIndex.Air:
                rotateTargetZ = 90;
                break;
            case RacesIndex.Earth:
                rotateTargetZ = 180;
                break;
            case RacesIndex.Water:
                rotateTargetZ = -90;
                break;
        }

        this.selectedButton = selectedButton;
        this.selectedRace = selectedRace;
        
        racePanel.SetDetail(Races.races[selectedRace].name, Races.races[selectedRace].color,
            Races.races[selectedRace].detail);

        transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotateTargetZ),
            160).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() => { LockButtons(true); });
    }

    public void LockButtons(bool isUnlocked)
    {
        btn_air.enabled = isUnlocked;
        btn_earth.enabled = isUnlocked;
        btn_fire.enabled = isUnlocked;
        btn_water.enabled = isUnlocked;
    }
}