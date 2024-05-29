using Assets.Script;
using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GoogleMobileAdsInit : MonoBehaviour
{
    public static GoogleMobileAdsInit Instance;
    public bool _Initialized;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            Init();
        }
    }
    public void Init()
    {
        StartCoroutine(StartAfterDB());
    }

    IEnumerator StartAfterDB()
    {

        if (AdsManager.Option_ShowAd)
        {

            yield return new WaitForEndOfFrame();

            //if (!ApplicationServices.ServiceIsReady)
            //    yield return new WaitUntil(() => !ApplicationServices.ServiceIsReady);
            //if (GameManager.instance == null)
            //    yield return new WaitUntil(() => GameManager.instance == null);
            //if (AdsManager.instance == null)
            //    yield return new WaitUntil(() => AdsManager.instance == null);

            try
            {
                // Listener

                MobileAds.Initialize(initStatus =>
                {
                    Log.Add("MobileAds Initialized");
                    Instance = this;
                    _Initialized = true;

                    Log.Add("Initialized Admob");
                    LoadBannerAd();
                    LoadInterstitialAd();
                    LoadRewardedAd();

                });
                DontDestroyOnLoad(this);
            }
            catch (Exception ex)
            {

                Log.Add($"{ex.Message}\n{ex.StackTrace}");
            }


        }
    }

    #region interstitial 

    private string _adUnitId = "ca-app-pub-1364965618349329/7670153213";

    private Action<bool> _onShowComplateinterstitialAd;

    private InterstitialAd _interstitialAd;
    public bool InterstitialAd_IsReady()
    {
        return _interstitialAd != null ? true : false;
    }
    public static int TryLoadInterstitial = 0;

    /// <summary>
    /// Loads the interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Log.Add("Loading the interstitial ad...");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Log.Add("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    if (TryLoadInterstitial < 5)
                    {
                        TryLoadInterstitial++;
                        LoadInterstitialAd();
                    }
                    return;
                }

                Log.Add("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo().GetResponseId());

                _interstitialAd = ad;
                RegisterEventHandlers(_interstitialAd);
            });
    }

    /// <summary>
    /// Shows the interstitial ad.
    /// </summary>
    public void ShowInterstitialAd(Action<bool> onShowComplate)
    {
        _onShowComplateinterstitialAd = onShowComplate;
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            _onShowComplateinterstitialAd?.Invoke(false);
        }
    }


    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            // Reload the ad so that we can show another as soon as possible.
            _onShowComplateinterstitialAd?.Invoke(true);

            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
    }



    #endregion

    #region BannerAd
    private static string _BannerAdUnitId = "ca-app-pub-1364965618349329/8726891072";
    static BannerView _bannerView;
    internal void Show()
    {
        if (_bannerView == null) return;

        try
        {
            _bannerView.Show();


        }
        catch (Exception ex)
        {
            Log.Add(ex.Message);
        }
    }

    internal void Hide()
    {
        if (_bannerView == null) return;

        try
        {

            _bannerView.Hide();
        }
        catch (Exception ex)
        {
            Log.Add(ex.Message);
        }
    }


    /// <summary>
    /// Creates a 320x50 banner view at top of the screen.
    /// </summary>
    public void CreateBannerView()
    {
        try
        {
            Log.Add("Creating banner view");

            // If we already have a banner, destroy the old one.
            if (_bannerView != null)
            {
                DestroyAd();
            }

            //var CanvasSize = Canvas_Banner_Simulator.GetComponent<RectTransform>().sizeDelta;

            // resulution 1280 * 800
            // Create a 320x50 banner at top of the screen
            //_bannerView = new BannerView(_BannerAdUnitId, AdSize.Banner,AdPosition.Bottom);

            //var bannersize = CalculateCanvasBannerSize();
            //_bannerView = new BannerView(_BannerAdUnitId, AdSize.Banner, ((int)CanvasSize.x / 2) , (int)CanvasSize.y - 25);
            _bannerView = new BannerView(_BannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
            ListenToAdEvents();

        }
        catch (Exception ex)
        {
            Log.Add(ex.StackTrace);
        }
    }

    /// <summary>
    /// Creates the banner view and loads a banner ad.
    /// </summary>
    public void LoadBannerAd()
    {
        // create an instance of a banner view first.
        if (_bannerView == null)
        {
            CreateBannerView();
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        Log.Add("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }
    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            Log.Add("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }


    /// <summary>
    /// listen to events the banner view may raise.
    /// </summary>
    private void ListenToAdEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView.OnBannerAdLoaded += () =>
        {
            Log.Add("Banner view loaded an ad with response : "
                + _bannerView.GetResponseInfo().GetResponseId());
            //if (GameElement._gameManager.InGame())
            //    _bannerView.Hide();
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Log.Add("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Log.Add(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Log.Add("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () =>
        {
            Log.Add("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Log.Add("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Log.Add("Banner view full screen content closed.");
        };
    }
    #endregion

    #region RewardedAd
    private static string _RewardedAdUnitId = "ca-app-pub-1364965618349329/3196482351";

    private static RewardedAd _rewardedAd;
    private Action<bool> _onShowCompleteRewardedAd;
    private static int TryLoadReward;


    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Log.Add("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_RewardedAdUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Log.Add("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    if (TryLoadReward < 5)
                    {
                        TryLoadReward++;
                        LoadRewardedAd();
                    }
                    return;
                }

                Log.Add("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo().GetResponseId());


                _rewardedAd = ad;

                RegisterEventHandlers(_rewardedAd);
            });
    }

    public void ShowRewardedAd(Action<bool> onShowComplete)
    {
        _onShowCompleteRewardedAd = onShowComplete;
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                //Log.Add(String.Format(rewardMsg, reward.Type, reward.Amount));
                //AdsManager.instance.OnComplateShowRewardedAd();
            });
        }
        else
        {
            onShowComplete?.Invoke(false);

            //AdsManager.instance.OnFailShowRewardedAd();
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //Log.Add(String.Format("Rewarded ad paid {0} {1}.",
            //    adValue.Value,
            //    adValue.CurrencyCode));

        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            //Log.Add("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            //Log.Add("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            //Log.Add("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            //Log.Add("Rewarded ad full screen content closed.");
            _onShowCompleteRewardedAd.Invoke(true);

            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Log.Add("Rewarded ad failed to open full screen content " +
            //               "with error : " + error);
            _onShowCompleteRewardedAd.Invoke(false);
            LoadRewardedAd();
        };
    }

    internal bool RewardedAdIsReady()
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

}
