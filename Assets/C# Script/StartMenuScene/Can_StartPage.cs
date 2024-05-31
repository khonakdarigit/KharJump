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

    // Start is called before the first frame update
    void Start()
    {
        Canvas_FadeAndRun.Instance.FadIn();


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

        Canvas_FadeAndRun.Instance.FadeOutAndRun(delegate
        {
            AdsManager.Instance.MyAdAndRun(delegate ()
             {
                 GameData.PlayCount++;
                 SceneManager.LoadScene(1);
             });
        });

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
