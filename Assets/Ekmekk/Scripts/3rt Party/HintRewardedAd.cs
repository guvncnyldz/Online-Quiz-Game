using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class HintRewardedAd : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public static HintRewardedAd instance;

    public Action OnEarnReward;
    
    private void Awake()
    {
        HintRewardedAd[] rewardedAds = FindObjectsOfType<HintRewardedAd>();

        if (rewardedAds.Length > 1)
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
    
    public void RequestInterstitial()
    {
        string adUnitId;
        
        #if UNITY_ANDROID
            adUnitId = "ca-app-pub-2366580648935894/1514974996";
        #elif UNITY_IPHONE
            adUnitId = "ca-app-pub-2366580648935894/8080383340";
        #else
            adUnitId = "unexpected_platform";
        #endif

        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        LoadAd();
    }

    void LoadAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
        
        LoadAd();
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        LoadAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received");
        
        OnEarnReward?.Invoke();
    }
    
    public void UserChoseToWatchAd()
    {
        return;
        if (this.rewardedAd.IsLoaded()) {
            this.rewardedAd.Show();
        }
        else
        {
            LoadAd();
        }
    }
}
