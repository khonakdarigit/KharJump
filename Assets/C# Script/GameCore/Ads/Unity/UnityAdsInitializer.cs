using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsInitializer : MonoBehaviour//, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    //public static UnityAdsInitializer instance;

    //[SerializeField] string _androidGameId;
    //private string _gameId;

    //[SerializeField] string _androidAdUnitId;
    //string _adUnitId = null; // This will remain null for unsupported platforms

    //public bool CanShowAd { get; private set; }

    //public bool InitializationComplete { get; set; }

    //void Start()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(this);
    //        InitializeAds();
    //    }
    //    else
    //    {
    //        if (GameManager.PlayGameCounter >= AdsManager.ShowRewardPerGame)
    //        {
    //            LoadAd();
    //        }
    //    }
    //}

    //public void InitializeAds()
    //{

    //    _gameId = _androidGameId;
    //    _adUnitId = _androidAdUnitId;


    //    if (!Advertisement.isInitialized && Advertisement.isSupported)
    //    {
    //        Advertisement.Initialize(_gameId, false, this);
    //    }
    //}
    //public void OnInitializationComplete()
    //{
    //    InitializationComplete = true;
    //    Log.Add("Unity Ads initialization complete.");

    //}
    //public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    //{
    //    Log.Add($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    //}

    //public void LoadAd()
    //{

    //    // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
    //    Log.Add("Loading Ad: " + _adUnitId);
    //    Advertisement.Load(_adUnitId, this);
    //}
    //// If the ad successfully loads, add a listener to the button and enable it:
    //public void OnUnityAdsAdLoaded(string adUnitId)
    //{
    //    CanShowAd = false;
    //    Log.Add("Ad Loaded: " + adUnitId);

    //    if (adUnitId.Equals(_adUnitId))
    //    {
    //        CanShowAd = true;
    //    }
    //}
    //// Implement a method to execute when the user clicks the button:
    //public void ShowAd()
    //{
    //    if (CanShowAd)
    //    {
    //        // Disable the button:
    //        // Then show the ad:
    //        Advertisement.Show(_adUnitId, this);
    //        CanShowAd = false;
    //    }
    //}
    //// Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    //public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    //{
    //    if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
    //    {
    //        Log.Add("Unity Ads Rewarded Ad Completed");
    //        Time.timeScale = 1;
    //        // Grant a reward.
    //    }
    //}
    //// Implement Load and Show Listener error callbacks:
    //public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    //{
    //    CanShowAd = false;

    //    Log.Add($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    //    // Use the error details to determine whether to try to load another ad.
    //}
    //public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    //{
    //    Log.Add($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    //    // Use the error details to determine whether to try to load another ad.
    //}
    //public void OnUnityAdsShowStart(string adUnitId) { Time.timeScale = 0; }
    //public void OnUnityAdsShowClick(string adUnitId) { }
    //void OnDestroy()
    //{
    //    // Clean up the button listeners:
    //    //_showAdButton.onClick.RemoveAllListeners();
    //}
}