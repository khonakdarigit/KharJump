using Assets.Script.Ads.Tapsell;
using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using TapsellPlusSDK;
using TapsellPlusSDK.models;
using UnityEngine;

public class TapsellAdsInit : MonoBehaviour
{
    private Action<bool> _onShowComplete;

    public static TapsellAdsInit Instance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAfterDB());

    }

    IEnumerator StartAfterDB()
    {

        if (AdsManager.Option_ShowAd)
        {

            if (Instance == null)
            {
                //if (!ApplicationServices.ServiceIsReady)
                //    yield return new WaitUntil(() => !ApplicationServices.ServiceIsReady);

                //if (GameManager.instance == null)
                //    yield return new WaitUntil(() => GameManager.instance == null);
                if (AdsManager.instance == null)
                    yield return new WaitUntil(() => AdsManager.instance == null);

                try

                {
                    DontDestroyOnLoad(this);


                    TapsellPlus.Initialize("rlqotkfcjhqtadbrpqlaktmjgffiblgskmgkoklmgdjsfsjpgjhmtehfkbbknsiqjmjmli",
                        adNetworkName =>
                        {
                            Log.Add(adNetworkName + " Initialized Successfully.");

                            Load_BannerAd();
                            //Load_RewardedAd();
                            Load_InterstitialAd(InterstitialType.banner);
                            //Load_InterstitialAd(InterstitialType.video);

                        },
                        error => Log.Add(error.ToString()));

                    Instance = this;


                }
                catch (Exception ex)
                {

                    Log.Add($"{ex.Message}\n{ex.StackTrace}");
                }

            }
        }
    }

    #region Banner
    private string _StandardBannerAd_responseId;

    private void Load_BannerAd()
    {
        try
        {
            Log.Add("BannerAd_Loading...");
            TapsellPlus.RequestStandardBannerAd("663a0c988703ba7a5edbd456", BannerType.Banner320X50,
                tapsellPlusAdModel =>
                {
                    Log.Add("on response " + tapsellPlusAdModel.responseId);
                    _StandardBannerAd_responseId = tapsellPlusAdModel.responseId;
                    //if (!GameManager.instance.InGame)
                    BannerAd_Show();
                },
                error =>
                {
                    Log.Add("Error " + error.message);
                    _StandardBannerAd_responseId = null;
                }
            );
        }
        catch (Exception ex)
        {

            Log.Add($"{ex.Message}\n{ex.StackTrace}");
        }

    }

    public void BannerAd_Show()
    {
        if (_StandardBannerAd_responseId != null)
        {
            TapsellPlus.ShowStandardBannerAd(_StandardBannerAd_responseId, Gravity.Bottom, Gravity.Center,
                tapsellPlusAdModel =>
                {
                    Log.Add("onOpenAd " + tapsellPlusAdModel.zoneId);
                    //if (GameManager.instance.InGame)
                    //TapsellPlus.HideStandardBannerAd();
                },
                error =>
                {
                    Log.Add("onError " + error.errorMessage);
                }
            );
        }

    }
    private int Banner_DisplayCounter;

    public void Banner_Display()
    {
        ///Debug.Log("");
        if (_StandardBannerAd_responseId != null)
        {
            TapsellPlus.DisplayStandardBannerAd();
            Banner_DisplayCounter++;
            if (Banner_DisplayCounter > 4)
            {
                Load_BannerAd();
                Banner_DisplayCounter = 0;
            }
        }
    }
    public void Banner_Hide()
    {
        if (_StandardBannerAd_responseId != null)
        {
            TapsellPlus.HideStandardBannerAd();
        }
    }


    #endregion


    #region Interstitial
    private string _Interstitial_Video_Ad_responseId;
    private string _Interstitial_Banner_Ad_responseId;
    private void Load_InterstitialAd(InterstitialType type)
    {
        Log.Add("InterstitialAd_Loading...");

        string key = "";
        switch (type)
        {
            case InterstitialType.video:
                key = "663a12cbabac1f7977d8baa1";
                break;
            case InterstitialType.banner:
                key = "6655c9098d647f7bb3b2ea8b";
                break;
            default:
                break;
        }

        TapsellPlus.RequestInterstitialAd(key,

                   tapsellPlusAdModel =>
                   {
                       Debug.Log("on response " + tapsellPlusAdModel.responseId);
                       switch (type)
                       {
                           case InterstitialType.video:
                               _Interstitial_Video_Ad_responseId = tapsellPlusAdModel.responseId;
                               break;
                           case InterstitialType.banner:
                               _Interstitial_Banner_Ad_responseId = tapsellPlusAdModel.responseId;
                               break;
                           default:
                               break;
                       }
                   },
                   error =>
                   {
                       switch (type)
                       {
                           case InterstitialType.video:
                               _Interstitial_Video_Ad_responseId = null;
                               break;
                           case InterstitialType.banner:
                               _Interstitial_Video_Ad_responseId = null;
                               break;
                           default:
                               break;
                       }
                       Debug.Log("Error " + error.message);
                   }
               );
    }

    public void ShowInterstitialAd(Action<bool> onShowComplete, InterstitialType type)
    {
        var _InterstitialAd_responseId = "";
        switch (type)
        {
            case InterstitialType.video:
                _InterstitialAd_responseId = _Interstitial_Video_Ad_responseId;
                break;
            case InterstitialType.banner:
                _InterstitialAd_responseId = _Interstitial_Banner_Ad_responseId;
                break;
            default:
                break;
        }

        _onShowComplete = onShowComplete;
        if (_InterstitialAd_responseId != null)
        {
            TapsellPlus.ShowInterstitialAd(_InterstitialAd_responseId,

                tapsellPlusAdModel =>
                {
                    Debug.Log("onOpenAd " + tapsellPlusAdModel.zoneId);
                },
                tapsellPlusAdModel =>
                {
                    Debug.Log("onCloseAd " + tapsellPlusAdModel.zoneId);
                    Load_InterstitialAd(type);
                    _onShowComplete?.Invoke(true);
                },
                error =>
                {
                    Debug.Log("onError " + error.errorMessage);
                    Load_InterstitialAd(type);
                    _onShowComplete?.Invoke(false);
                }
                );
        }
        else
        {
            _onShowComplete?.Invoke(false);
        }


    }
    #endregion

    #region Rewarded
    private string _RequestRewardedAd_responseId;


    private void Load_RewardedAd()
    {
        Log.Add("RewardedAd_Loading...");

        TapsellPlus.RequestRewardedVideoAd("663a12f819a7741554bc302d",

                   tapsellPlusAdModel =>
                   {
                       Debug.Log("on response " + tapsellPlusAdModel.responseId);
                       _RequestRewardedAd_responseId = tapsellPlusAdModel.responseId;
                   },
                   error =>
                   {
                       Debug.Log("Error " + error.message);
                       _RequestRewardedAd_responseId = null;
                   }
               );
    }


    public void ShowRewardedAd(Action<bool> onShowComplate)
    {
        _onShowComplete = onShowComplate;
        TapsellPlus.ShowRewardedVideoAd(_RequestRewardedAd_responseId,

           tapsellPlusAdModel =>
           {
               Log.Add("onOpenAd " + tapsellPlusAdModel.zoneId);
           },
           tapsellPlusAdModel =>
           {
               Log.Add("onRewardedAd " + tapsellPlusAdModel.zoneId);
           },
           tapsellPlusAdModel =>
           {
               Log.Add("onCloseAd " + tapsellPlusAdModel.zoneId);
               Load_RewardedAd();
               _onShowComplete?.Invoke(true);
           },
           error =>
           {
               Log.Add("onError " + error.errorMessage);
               Load_RewardedAd();
               _onShowComplete?.Invoke(false);
           }
       );

    }

    internal bool RewardedAdIsReady()
    {
        return _RequestRewardedAd_responseId == null ? false : true;
    }

    internal bool Banner_isReady()
    {
        return _StandardBannerAd_responseId == null ? false : true;
    }

    internal bool InterstitialAd_IsReady(InterstitialType type)
    {
        bool ready = false;
        switch (type)
        {
            case InterstitialType.video:
                ready = _Interstitial_Video_Ad_responseId == null ? false : true;
                break;
            case InterstitialType.banner:
                ready = _Interstitial_Banner_Ad_responseId == null ? false : true;
                break;
            default:
                break;
        }

        return ready;
    }

    #endregion
}
