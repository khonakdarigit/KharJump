using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can_PuseMenu : MonoBehaviour
{
    public static Can_PuseMenu instance;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) { instance = this; }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btn_Resume()
    {
        Hide();
        App.instance.Resume();
    }

    internal void Show()
    {
        Can_GameUI.instance.toSystemSetingScreeTimeOut();
        panel.SetActive(true);
    }

    internal void Hide()
    {
        Can_GameUI.instance.toNeverSleepScreeTimeOut();

        panel.SetActive(false);
    }

    public void btn_BackToMenu()
    {
        App.instance.Resume();
        App.instance.Show_Menu();
    }
}