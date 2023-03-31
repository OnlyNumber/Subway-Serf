using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class RewardedR : MonoBehaviour
{
    private RewardedAd rewardedAd;
    private const string adUnitId = "ca-app-pub-3940256099942544/5224354917";


    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequesRewardVideo();


    }

    private void RequesRewardVideo()
    {
        rewardedAd = new RewardedAd(adUnitId);
        rewardedAd.OnUserEarnedReward += HandeUserEarnedReward;
        rewardedAd.OnAdClosed += HandlerRewardedAdClosed;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;

        AdRequest request = new AdRequest.Builder().Build();

        rewardedAd.LoadAd(request);

    }

    public void ShowRewardVideo()
    {
        if(rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        RequesRewardVideo();
    }

    public void HandlerRewardedAdClosed(object sender, EventArgs args)
    {
        RequesRewardVideo();
    }

    public void HandeUserEarnedReward(object sender, Reward args)
    {
        RequesRewardVideo();
    }

}
