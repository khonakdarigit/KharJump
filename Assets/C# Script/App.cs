using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{
    public GameController gameController;
    public static App instance;
    public int maxScore;
    public int liveScore;
    public UnityEngine.UI.Text txt_Score;

    public AudioSource
        Explosion;

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

    // Start is called before the first frame update
    void Start()
    {
        isCoroutineExecuting = false;
        if (instance == null) { instance = this; }


    }

    public void PlayNewGame()
    {
        SceneManager.LoadScene(1);
    }

    internal void Pause()
    {

        Can_PuseMenu.instance.Show();
        Time.timeScale = 0f;

        if (maxScore > Progress.instance._playerInfo.Record)
        {
            Progress.instance.Save(maxScore);
            //gameController.ChangeScore(maxScore);
        }
        API.instance.AddNewScore(liveScore);

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
        if (maxScore > Progress.instance._playerInfo.Record)
        {
            Progress.instance.Save(maxScore);
            //gameController.ChangeScore(maxScore);
        }

        //API.instance.AddNewScore(liveScore);
    }

    internal void ChangeTxtScore(int score)
    {
        liveScore = score;
        if (liveScore > maxScore)
        {
            maxScore = liveScore;
            txt_Score.text = String.Format("Score : {0} m", score);

        }


    }



}
