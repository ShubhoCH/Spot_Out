using Yodo1.MAS;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EMAG_Yodo : MonoBehaviour
{
    public static EMAG_Yodo instance;
    void Start()
    {
        Yodo1U3dMas.SetCOPPA(false);
        Yodo1U3dMas.SetGDPR(true);
        Yodo1U3dMas.SetCCPA(false);
        Yodo1U3dMas.SetInitializeDelegate((bool success, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] InitializeDelegate, success:" + success + ", error: \n" + error.ToString());

            if (success)
            {// Initialize successful
            }
            else
            { // Initialize failure

            }
        });
        Yodo1U3dMas.InitializeSdk();
        //Initialize_Banner();
        Initialize_Interstitial();
        Initialize_RewardedVideo();

        //Make an Undestroyable Instance:
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    void Initialize_Banner()
    {
        Yodo1U3dMas.SetBannerAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] BannerdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Banner ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Banner ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Banner ad error, " + error.ToString());
                    break;
            }
        });
    }
    void Initialize_Interstitial()
    {
        Yodo1U3dMas.SetInterstitialAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] InterstitialAdDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Interstital ad has been shown.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Interstital ad error, " + error.ToString());
                    break;
            }

        });
    }
    void Initialize_RewardedVideo()
    {
        Yodo1U3dMas.SetRewardedAdDelegate((Yodo1U3dAdEvent adEvent, Yodo1U3dAdError error) =>
        {
            Debug.Log("[Yodo1 Mas] RewardVideoDelegate:" + adEvent.ToString() + "\n" + error.ToString());
            switch (adEvent)
            {
                case Yodo1U3dAdEvent.AdClosed:
                    Debug.Log("[Yodo1 Mas] Reward video ad has been closed.");
                    break;
                case Yodo1U3dAdEvent.AdOpened:
                    Debug.Log("[Yodo1 Mas] Reward video ad has shown successful.");
                    break;
                case Yodo1U3dAdEvent.AdError:
                    Debug.Log("[Yodo1 Mas] Reward video ad error, " + error);
                    break;
                case Yodo1U3dAdEvent.AdReward:
                    Debug.Log("[Yodo1 Mas] Reward video ad reward, give rewards to the player.");
                    if (SceneManager.sceneCount == 1)
                    {
                        var manager = GameObject.Find("MANAGER").GetComponent<Manager>();
                        //manager.TimeUp_Rewarded();
                    }
                    break;
            }

        });
    }
    //public void Show_Banner()
    //{
    //    if (Yodo1U3dMas.IsBannerAdLoaded())
    //    {
    //        int align = Yodo1U3dBannerAlign.BannerTop;
    //        //int align = Yodo1U3dBannerAlign.BannerTop | Yodo1U3dBannerAlign.BannerHorizontalCenter;
    //        Yodo1U3dMas.ShowBannerAd(align);
    //        Debug.Log("Banner Shownnnnnnnnnnnnn");
    //    }
    //    else
    //    {
    //        Debug.Log("[Yodo1 Mas] Banner ad has not been cached.");
    //    }
    //}
    //public void Close_Banner() => Yodo1U3dMas.DismissBannerAd();
    public void Show_Interstitial()
    {
        if (Yodo1U3dMas.IsInterstitialAdLoaded())
        {
            Yodo1U3dMas.ShowInterstitialAd();
        }
        else
        {
            Debug.Log("[Yodo1 Mas] Interstitial ad has not been cached.");
        }
    }
    public void Show_RewardedVideo()
    {
        if (Yodo1U3dMas.IsRewardedAdLoaded())
        {
            Yodo1U3dMas.ShowRewardedAd();
            var manager = GameObject.Find("MANAGER").GetComponent<Manager>();
            manager.Reward();
        }
        else
        {
            Handheld.Vibrate();
            Debug.Log("[Yodo1 Mas] Reward video ad has not been cached.");
        }
    }
}
