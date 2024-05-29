using Assets.DataLayer;
using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Can_StartPage : MonoBehaviour
{
    public UnityEngine.UI.Text txtScore;
    public UnityEngine.UI.Button btn_Play_obg;
    public UnityEngine.UI.Text txt_Version;
    public static PolishFor polishFor = PolishFor.Myket;

    private float time;
    private Action _FadeAndLoadGame;


    // Start is called before the first frame update
    void Start()
    {
        txtScore.text = string.Format("Your Score: {0} m", ApplicationServices.playerInfoService.GetPlayerInfo().Record);
        txt_Version.text = "Version : " + Application.version.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btn_Play()
    {
        GameController.Instance.ButtonClickSound();

        Action doThat = new Action(delegate ()
        {
            GameData.PlayCount++;
            SceneManager.LoadScene(1);

        });


        MyAdAndRun(delegate ()
        {
            doThat.Invoke();

        });
    }

    private void MyAdAndRun(Action value)
    {
        _FadeAndLoadGame = value;
        if (GameData.PlayCount == AdsManager.Option_ShowInterstitialPerGame)
        {
            GameData.PlayCount = 0;

            if (AdsManager.instance.InterstitialAdIsReady(Assets.Script.Ads.Tapsell.InterstitialType.banner))
                AdsManager.instance.ShowInterstitialAd(OnShowComplete, Assets.Script.Ads.Tapsell.InterstitialType.banner);
            else
                _FadeAndLoadGame?.Invoke();
        }
        else
        {
            _FadeAndLoadGame?.Invoke();
        }

    }
    public void OnShowComplete(bool ShowComplete)
    {
        Log.Add("OnShowComplete");
        //Progress.instance.AddLogWithApi($"{this.GetType().Name}/{MethodBase.GetCurrentMethod().Name}", logLevel.Info, $"ShowInterstitialAd Complete status :{ShowComplete}");
        _FadeAndLoadGame?.Invoke();
    }


    public void Contact()
    {
        GameController.Instance.ButtonClickSound();
        try
        {

            Application.OpenURL(Helper.AppContactUrl());

        }
        catch (Exception ex)
        {
            Log.Add(ex.Message);
        }
    }

    public void btn_Exit()
    {
        StartCoroutine(App.Instance.ExecuteAfterTime(0.2f, () =>
        {
            Application.Quit();
        }));
    }
}
