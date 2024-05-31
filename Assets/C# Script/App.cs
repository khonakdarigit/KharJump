using Assets.C__Script;
using Assets.C__Script.GameCore.Api;
using Assets.DataLayer;
using Assets.DataLayer.Infrastructure.Services;
using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{
    public GameController gameController;
    public int maxScore;
    public int liveScore;
    public UnityEngine.UI.Text txt_Score;
    public static App Instance;


    public AudioSource
        Explosion;

    private void Start()
    {
        Instance = this;
        Canvas_FadeAndRun.Instance.FadIn();
    }
    public void PlayExplosion()
    {
        Explosion.Play();
    }

    public bool IsGameOver;



    internal void ChangeStatus(string staus)
    {
        //txt_Debug.text += String.Format("\nDonky.DonkeyStaus : {0}", staus);
    }

    private bool isCoroutineExecuting;

    public void PlayNewGame()
    {
        SceneManager.LoadScene(1);
    }

    internal void Pause()
    {

        Can_PuseMenu.instance.Show();
        Time.timeScale = 0f;

        CheckRecordAndSave();

    }

    private void CheckRecordAndSave()
    {
        var plyer = ApplicationServices.playerInfoService.GetPlayerInfo();

        if (maxScore > plyer.Record)
        {
            plyer.Record = maxScore;
            ApplicationServices.playerInfoService.UpdatePlayerInfo(plyer);
            BinoGameServiceApi_LeaderBoard.Instance.Api_AddScoreToLeaderBord();
        }
    }

    internal void Resume()
    {
        Time.timeScale = 1f;
    }

    internal void Show_Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        IsGameOver = true;
        CameraFollow.instance.FollowDonkeyOnGameOver();

        StartCoroutine(ExecuteAfterTime(0.8f, () =>
        {
            SaveData();
            Can_GameOver.instance.Show();
            CameraFollow.instance.UnFollowDonkeyOnGameOver();
        }));

        StartCoroutine(ExecuteAfterTime(0.5f, () =>
        {
            Time.timeScale = 0f;

        }));

    }

    public IEnumerator ExecuteAfterTime(float time, Action task)
    {
        if (isCoroutineExecuting)
            yield break;
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        isCoroutineExecuting = false;
    }


    internal void SaveData()
    {
        CheckRecordAndSave();
    }

    internal void ChangeTxtScore(int score)
    {
        liveScore = score;
        if (liveScore > maxScore)
        {
            maxScore = liveScore;
            txt_Score.text = $"{score} m";
        }


    }



}
