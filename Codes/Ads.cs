using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ads : MonoBehaviour
{
    public Manager manager;
    BannerView banner;
    public InterstitialAd interstitialAd;
    public RewardedAd hammerAd;
    public RewardedAd changeAd;

    //string APP_ID = "your-app-id";


    void Start()
    {
        MobileAds.Initialize(initStatus => { });

        BannerRequest();
        InterstitialRequest();
        HammerRequest();
        ChangeRequest();
    }


    public void BannerRequest()
    {
        string bannerId = "your-banner-id";
        banner = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);

        if (PlayerPrefs.GetInt("RemoveAd") == 0)
        {
            AdRequest adRequest = new AdRequest.Builder().Build();
            banner.LoadAd(adRequest);
        }
    }


    public void InterstitialRequest()
    {
        string adUnitId = "your-interstitial-id";
        interstitialAd = new InterstitialAd(adUnitId);

        if (PlayerPrefs.GetInt("RemoveAd") == 0)
        {
            AdRequest interstitialAdRequest = new AdRequest.Builder().Build();
            interstitialAd.LoadAd(interstitialAdRequest);
        }
    }


    public void HammerRequest()
    {
        string adUnitId = "your-rewarded-id";
        hammerAd = new RewardedAd(adUnitId);

        hammerAd.OnUserEarnedReward += HammerAd_OnUserEarnedReward;

        AdRequest hammerRequest = new AdRequest.Builder().Build();
        hammerAd.LoadAd(hammerRequest);
    }


    public void HammerAd_OnUserEarnedReward(object sender, Reward e)
    {
        string type = e.Type;
        double amount = e.Amount;
        print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);

        manager.Hammer(false);
    }


    public void ChangeRequest()
    {
        string adUnitId = "your-rewarded-id";
        changeAd = new RewardedAd(adUnitId);

        changeAd.OnUserEarnedReward += ChangeAd_OnUserEarnedReward;

        AdRequest changeRequest = new AdRequest.Builder().Build();
        changeAd.LoadAd(changeRequest);
    }


    public void ChangeAd_OnUserEarnedReward(object sender, Reward e)
    {
        string type = e.Type;
        double amount = e.Amount;
        print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);

        manager.Change(false);
    }
}
