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
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btn_PlayItAgain()
    {
        GameController.Instance.ButtonClickSound();

        Canvas_FadeAndRun.Instance.FadeOutAndRun(delegate
        {
            AdsManager.Instance.MyAdAndRun(delegate
            {
                Hide();
                App.Instance.Resume();
                App.Instance.PlayNewGame();
                GameData.PlayCount++;
            });
        });
    }


    //btn_BackToManu

    public void btn_BackToManu()
    {
        GameController.Instance.ButtonClickSound();

        Canvas_FadeAndRun.Instance.FadeOutAndRun(delegate
        {
            App.Instance.Resume();
            App.Instance.Show_Menu();
        });
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
