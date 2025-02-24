﻿using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class Interstitial : MonoBehaviour
{
    public static Interstitial instance;

    private InterstitialAd interstitial;

    private void Awake()
    {
        Interstitial[] interstitial = FindObjectsOfType<Interstitial>();

        if (interstitial.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            RequestInterstitial();
        }
    }

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-2366580648935894/7158751162";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-2366580648935894/6967179477";
#else
        string adUnitId = "unexpected_platform";
#endif

        this.interstitial = new InterstitialAd(adUnitId);

        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.interstitial.OnAdClosed += HabdleOnAdClosed;
        LoadAd();
    }

    private void HabdleOnAdClosed(object sender, EventArgs e)
    {
        LoadAd();
    }

    void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);

        LoadAd();
    }


    void LoadAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    public void ShowAd()
    {
        return;
        if (DataManager.GameSeries >= 3)
        {
            if (!interstitial.IsLoaded())
            {
                LoadAd();
            }
            else
            {
                interstitial.Show();
                DataManager.GameSeries = 0;
            }
        }
        else
        {
            DataManager.GameSeries += 1;
        }
    }
}