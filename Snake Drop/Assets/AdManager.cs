using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;
    private RewardedAd undoAd;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        CreateAd();
    }

    public void UserChoseToWatchAd()
    {
        if (undoAd.IsLoaded())
        {
            undoAd.Show();
        }
    }

    public void CreateAd()
    {
        undoAd = CreateRewardedAd();
    }


    private RewardedAd CreateRewardedAd()
    {
        string adUnitID = "ca-app-pub-3940256099942544/5224354917";
        RewardedAd rewardedAd = new RewardedAd(adUnitID);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
        return rewardedAd;
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args){}

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) { }

    public void HandleRewardedAdOpening(object sender, EventArgs args) { }
    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args) { }
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        CreateAd();
        GameManager.instance.pauseManager.UnPause();
    }
    public void HandleUserEarnedReward(object sender, Reward args) 
    {
        GameManager.instance.playerManagers[0].Undoer.GetUndos();
    }



}
