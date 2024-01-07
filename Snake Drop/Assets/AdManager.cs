using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    //TEST ID
    //private string adUnitID = "ca-app-pub-3940256099942544/5224354917";

    //LIVE ID
    private string adUnitID = "ca-app-pub-5711654782694273/6943773560";



    public static AdManager instance;
    private RewardedAd undoAd;
    bool autoUndo;
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
        if (undoAd != null && undoAd.CanShowAd())
        {
            autoUndo = false;
            undoAd.Show((Reward reward) =>
            {
                HandleUserEarnedReward();
            });

        }
    }

    public void UserChoseToWatchAdAndUndo()
    {
        if (undoAd != null && undoAd.CanShowAd())
        {
            autoUndo = true;
            undoAd.Show((Reward reward) =>
            {
                HandleUserEarnedReward();
            });

        }
    }
    public void CreateAd()
    {


        if (undoAd != null)
        {
            undoAd.Destroy();
            undoAd = null;
        }
        Debug.Log("Loading the rewarded ad.");


        AdRequest adRequest = new AdRequest.Builder().Build();

        RewardedAd.Load(adUnitID, adRequest,
          (RewardedAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("Rewarded ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Rewarded ad loaded with response : "
                        + ad.GetResponseInfo());

              undoAd = ad;
              undoAd.OnAdFullScreenContentClosed += HandleRewardedAdClosed;
          });

        


    }

    public void HandleRewardedAdClosed()
    {
        undoAd.Destroy();
        CreateAd();
        GameManager.instance.pauseManager.UnPause();
    }
    public void HandleUserEarnedReward() 
    {
        GameManager.instance.playerManagers[0].Undoer.GetUndos();
        if(autoUndo) GameManager.instance.playerManagers[0].Undoer.TryUndo();
    }



}
