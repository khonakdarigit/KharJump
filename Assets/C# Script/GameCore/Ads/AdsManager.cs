using Assets.Script.Ads.Tapsell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class AdsManager : MonoBehaviour
{

    public static AdsManager instance;

    static public AdsType Option_Ads_Interstitial_Ad_Type = AdsType.Tapsell;
    static public AdsType Option_Ads_Rewarded_Ad_Type = AdsType.Tapsell;
    static public AdsType Option_Ads_Banner_Ad_Type = AdsType.Tapsell;

    public static int Option_ShowInterstitialPerGame = 4;
    public static int Option_ShowRewardPerGame = 5;
    public static int Option_GiftRewardPerDay = 6;
    public static bool Option_ShowAd = true;


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            StartCoroutine(StartAfterDB());
            DontDestroyOnLoad(this);
        }
    }

    IEnumerator StartAfterDB()
    {
        yield return new WaitForEndOfFrame();

        //if (!ApplicationServices.ServiceIsReady)
        //    yield return new WaitUntil(() => !ApplicationServices.ServiceIsReady);

        //if (TapsellAdsInit.Instance == null)
        //    yield return new WaitUntil(() => TapsellAdsInit.Instance == null);
        //if (Admob.Instance == null)
        //    yield return new WaitUntil(() => AdColonyInit.Instance == null);

        Option_ShowAd = true; //Progress._playerInfo.ShowAd;
        //MyAd_AdMob.Instance.Init();
        //AdColonyInit.Instance.Init();
    }



    private bool BannerAdIsReady()
    {
        bool result = false;
        switch (Option_Ads_Banner_Ad_Type)
        {
            case AdsType.UnityAds:
                break;
            case AdsType.Admob:
                //result = MyAd_AdMob.Instance.Banner_isReady();
                break;
            case AdsType.Adivery:
                break;
            case AdsType.Tapsell:
                result = TapsellAdsInit.Instance.Banner_isReady();
                break;
            case AdsType.AdColony:
                break;
            default:
                break;
        }
        return result;
    }

    internal static void HideBannerAd()
    {
        try
        {
            switch (Option_Ads_Banner_Ad_Type)
            {
                case AdsType.UnityAds:
                    break;
                case AdsType.Admob:
                    GoogleMobileAdsInit.Instance.Hide();
                    break;
                case AdsType.Adivery:
                    //AdiveryInit.Instance.HideBannerAd();
                    break;
                case AdsType.Tapsell:
                    TapsellAdsInit.Instance.Banner_Hide();
                    break;
                case AdsType.AdColony:
                    //AdColonyInit.Instance.Banner_Hide();
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Add(ex.StackTrace);
        }

    }

    internal static void ShowBannerAd()
    {
        if (!Option_ShowAd) return;


        try
        {
            switch (Option_Ads_Banner_Ad_Type)
            {
                case AdsType.UnityAds:
                    break;
                case AdsType.Admob:
                    GoogleMobileAdsInit.Instance.Show();
                    break;
                case AdsType.Adivery:
                    //AdiveryInit.Instance.ShowBannerAd();
                    break;
                case AdsType.Tapsell:
                    TapsellAdsInit.Instance.Banner_Display();
                    break;
                case AdsType.AdColony:
                    //AdColonyInit.Instance.Banner_Display();
                    break;
                default:
                    break;
            }


        }
        catch (Exception ex)
        {
            Log.Add(ex.StackTrace);
        }

    }

    public bool InterstitialAdIsReady(InterstitialType tapselType = InterstitialType.banner)
    {
        bool IsReady = false;
        switch (Option_Ads_Interstitial_Ad_Type)
        {
            case AdsType.UnityAds:
                break;
            case AdsType.Admob:
                IsReady = GoogleMobileAdsInit.Instance.InterstitialAd_IsReady();

                break;
            case AdsType.Adivery:
                break;
            case AdsType.Tapsell:
                IsReady = TapsellAdsInit.Instance.InterstitialAd_IsReady(tapselType);
                break;
            case AdsType.AdColony:
                break;
            default:
                break;
        }
        return IsReady;
    }

    internal void ShowInterstitialAd(Action<bool> onShowComplete, InterstitialType tapsellType)
    {
        if (!Option_ShowAd) return;

        try
        {
            switch (Option_Ads_Interstitial_Ad_Type)
            {
                case AdsType.UnityAds:
                    break;
                case AdsType.Admob:
                    GoogleMobileAdsInit.Instance.ShowInterstitialAd(onShowComplete);
                    break;
                case AdsType.Adivery:
                    //AdiveryInit.Instance.ShowInterstitialAd(onShowComplate);
                    break;
                case AdsType.Tapsell:
                    TapsellAdsInit.Instance.ShowInterstitialAd(onShowComplete, tapsellType);
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Add(ex.StackTrace);
        }


    }

    internal bool RewardedAdIsReady()
    {

        if (!Option_ShowAd) return false;


        var isReady = false;
        try
        {
            switch (Option_Ads_Rewarded_Ad_Type)
            {
                case AdsType.UnityAds:
                    break;
                case AdsType.Admob:
                    isReady = GoogleMobileAdsInit.Instance == null ? false : GoogleMobileAdsInit.Instance.RewardedAdIsReady();
                    break;
                case AdsType.Adivery:
                    break;
                case AdsType.Tapsell:
                    break;
                case AdsType.AdColony:
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Add(ex.StackTrace);
        }

        return isReady;
    }

    internal void ShowRewardedAd(Action<bool> onShowComplate)
    {
        if (!Option_ShowAd) return;

        try
        {
            switch (Option_Ads_Rewarded_Ad_Type)
            {
                case AdsType.UnityAds:
                    //if (UnityAdsInitializer.instance.CanShowAd)
                    //{
                    //    UnityAdsInitializer.instance.ShowAd();
                    //}
                    break;
                case AdsType.Admob:
                    GoogleMobileAdsInit.Instance.ShowRewardedAd(onShowComplate);
                    break;
                case AdsType.Adivery:
                    //AdiveryInit.Instance.ShowRewardedAd(onShowComplate);
                    break;
                case AdsType.Tapsell:
                    TapsellAdsInit.Instance.ShowRewardedAd(onShowComplate);
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Add(ex.StackTrace);
        }


    }

}

