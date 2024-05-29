using Assets.DataLayer;
using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Can_GameOver : MonoBehaviour
{
    public static Can_GameOver instance;
    public GameObject panel;
    public Text txtScore;
    private Action _FadeAndLoadGame;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) { instance = this; }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btn_PlayItAgain()
    {
        GameController.Instance.ButtonClickSound();

        Action doThat = new Action(delegate ()
        {
            Hide();
            App.Instance.Resume();
            App.Instance.PlayNewGame();
            GameData.PlayCount++;
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



    //btn_BackToManu

    public void btn_BackToManu()
    {
        GameController.Instance.ButtonClickSound();

        App.Instance.Resume();
        App.Instance.Show_Menu();
    }
    internal void Show()
    {
        Can_GameUI.instance.toSystemSetingScreeTimeOut();
        panel.SetActive(true);
        txtScore.text = String.Format("Your score: {0}\nYour high score: {1}", App.Instance.maxScore, ApplicationServices.playerInfoService.GetPlayerInfo().Record);
    }
    internal void Hide()
    {
        Can_GameUI.instance.toNeverSleepScreeTimeOut();
        panel.SetActive(true);
    }
}
