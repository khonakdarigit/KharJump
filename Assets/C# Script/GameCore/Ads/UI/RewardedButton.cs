//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
//using UnityEngine;
//using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

//public class RewardedButton : MonoBehaviour
//{
//    public static RewardedButton intance;
//    private bool _rewarUserBeforPlayDestory;

//    // Start is called before the first frame update
//    void Start()
//    {
//        if (intance == null)
//            intance = this;

//        StartCoroutine(StartAfterDB());
//    }

//    IEnumerator StartAfterDB()
//    {
//        if (!Progress.DatabaseIsRedy)
//            yield return new WaitUntil(() => !Progress.DatabaseIsRedy);
//        if (GameManager.instance == null)
//            yield return new WaitUntil(() => GameManager.instance == null);
//        if (AdsManager.instance == null)
//            yield return new WaitUntil(() => AdsManager.instance == null);


//        if (AdsManager.Option_ShowAd & GameManager.CanReward() & AdsManager.instance.RewardedAdIsReady())
//        {
//            gameObject.SetActive(true);
//        }
//        else
//        {
//            gameObject.SetActive(false);
//        }
//    }

//    public void OnGetRewarded()
//    {
//        AdsManager.instance.ShowRewardedAd(OnShowComplate);
//    }
//    public void OnShowComplate(bool ShowComplete)
//    {
//        Log.Add($"OnShowComplate : {ShowComplete}");
//        Progress.instance.AddLogWithApi($"{this.GetType().Name}/{MethodBase.GetCurrentMethod().Name}", logLevel.Info, $"ShowRewardedAd Complete status :{ShowComplete}");

//        if (ShowComplete)
//        {
//            _rewarUserBeforPlayDestory = true;

//            GameManager.instance.AddCoin(40000);
//            GameManager.instance.AddDim(5);

//            gameObject.SetActive(false);
//        }
//    }



//    private void OnDestroy()
//    {
//        if (_rewarUserBeforPlayDestory)
//        {
//            GameManager.instance.SaveGameMony();
//            GameManager.instance.NewRewardLog();
//            _rewarUserBeforPlayDestory = false;
//        }
//    }



//}
