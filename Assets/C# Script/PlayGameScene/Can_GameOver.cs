using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Can_GameOver : MonoBehaviour
{
    public static Can_GameOver instance;
    public GameObject panel;
    public Text txtScore;
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
        Hide();
        App.instance.Resume();
        App.instance.PlayNewGame();
    }

    //btn_BackToManu

    public void btn_BackToManu()
    {
        App.instance.Resume();
        App.instance.Show_Menu();
    }
    internal void Show()
    {
        Can_GameUI.instance.toSystemSetingScreeTimeOut();
        panel.SetActive(true);
        txtScore.text = String.Format("Your score: {0}\nYour high score: {1}", App.instance.maxScore, Progress.instance._playerInfo.Record);
    }
    internal void Hide()
    {
        Can_GameUI.instance.toNeverSleepScreeTimeOut();
        panel.SetActive(true);
    }
}
