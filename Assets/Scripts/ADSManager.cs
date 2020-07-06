using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class ADSManager : MonoBehaviour {

    public static ADSManager instance;

    readonly private string sotreID = "3249714";
    readonly private bool testMode = false;

    readonly private string videoAD = "video";
    readonly private string rewardedVideoAD = "rewardedVideo";

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        Monetization.Initialize(sotreID, testMode);
    }

    public void ShowVideoAD()
    {
        if(Monetization.IsReady(videoAD))
        {
            ShowAdPlacementContent ad = Monetization.GetPlacementContent(videoAD) as ShowAdPlacementContent;

            if(ad != null)
            {
                ad.Show();
            }
        }
    }

    public void ShowRewardedVideoAD()
    {
        if (Monetization.IsReady(rewardedVideoAD))
        {
            ShowAdPlacementContent ad = Monetization.GetPlacementContent(rewardedVideoAD) as ShowAdPlacementContent;

            if (ad != null)
            {
                ad.Show();
            }
        }
    }
}
