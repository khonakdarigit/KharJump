using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IAdSource
{
    
    void Init();
    bool Initialized();
    void LoadAds();

    #region Banner
    void Banner_Display();
    void Banner_Hide();
    bool Banner_isReady();
    #endregion

    #region Interstital
    void InterstitialAd_Show(Action<bool> onShowComplate);
    void InterstitialAd_Closed(bool Status);
    bool InterstitialAd_IsReady();
    #endregion

    #region Rewarded
    void RewardedAd_Show(Action<bool> onShowComplate);
    void RewardedAd_GetReward();
    void RewardedAd_Closed();
    bool Rewarded_IsReady();
    #endregion
}

