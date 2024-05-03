using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reklamlar : MonoBehaviour
{
    public Yonetim yonetim;
    BannerView banner;
    public InterstitialAd gecis;
    public RewardedAd cekicReklam;
    public RewardedAd degisimReklam;

    //string APP_ID = "your-app-id";


    void Start()
    {
        MobileAds.Initialize(initStatus => { });

        BannerSorgu();
        GecisSorgu();
        CekicSorgu();
        DegisimSorgu();
    }


    public void BannerSorgu()
    {
        string bannerId = "your-banner-id";
        banner = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);

        if (PlayerPrefs.GetInt("ReklamKaldir") == 0)
        {
            AdRequest reklamSorgusu = new AdRequest.Builder().Build();
            banner.LoadAd(reklamSorgusu);
        }
    }


    public void GecisSorgu()
    {
        string adUnitId = "your-interstitial-id";
        gecis = new InterstitialAd(adUnitId);

        if (PlayerPrefs.GetInt("ReklamKaldir") == 0)
        {
            AdRequest gecisSorgusu = new AdRequest.Builder().Build();
            gecis.LoadAd(gecisSorgusu);
        }
    }


    public void CekicSorgu()
    {
        string adUnitId = "your-rearded-id";
        cekicReklam = new RewardedAd(adUnitId);

        cekicReklam.OnUserEarnedReward += CekicReklam_OnUserEarnedReward;

        AdRequest cekicSorgusu = new AdRequest.Builder().Build();
        cekicReklam.LoadAd(cekicSorgusu);
    }


    public void CekicReklam_OnUserEarnedReward(object sender, Reward e)
    {
        string type = e.Type;
        double amount = e.Amount;
        print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);

        yonetim.Cekic(false);
    }


    public void DegisimSorgu()
    {
        string adUnitId = "your-rewarded-id";
        degisimReklam = new RewardedAd(adUnitId);

        degisimReklam.OnUserEarnedReward += DegisimReklam_OnUserEarnedReward;

        AdRequest degisimSorgusu = new AdRequest.Builder().Build();
        degisimReklam.LoadAd(degisimSorgusu);
    }


    public void DegisimReklam_OnUserEarnedReward(object sender, Reward e)
    {
        string type = e.Type;
        double amount = e.Amount;
        print("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);

        yonetim.Degisim(false);
    }
}
