using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can_GameUI : MonoBehaviour
{
    public static Can_GameUI instance;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {


        toNeverSleepScreeTimeOut();
        instance = this;

    }


    public void toSystemSetingScreeTimeOut()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }
    public void toNeverSleepScreeTimeOut()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void btn_Pause()
    {
        GameController.Instance.ButtonClickSound();
        PlayGameScene.Instance.Pause();
    }


}
