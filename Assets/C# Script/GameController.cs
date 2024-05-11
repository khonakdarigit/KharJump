using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;
using UnityEngine.SceneManagement;
using System;
using Assets;

public class GameController : MonoBehaviour
{
    public GameObject optionCanvas;
    public GameObject topScoreCanvas;

    public TMPro.TMP_InputField optionCanvas_textUserName;
    public Sprite optionCanvas_MusicOnSprint;
    public Sprite optionCanvas_MusicOffSprint;
    public UnityEngine.UI.Image optionCanvas_MusicOnOffButton;


    public AudioSource BackMusic;
    public AudioSource buttonClickAudioSource;

    public static bool GameMute;

    public static GameController instance;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void ButtonClickSound()
    {
        buttonClickAudioSource.Play();
    }

    public void PlayButtonClick()
    {
        buttonClickAudioSource.Play();

        SceneManager.LoadScene(1);
    }

    public void OptionButtonClick()
    {
        buttonClickAudioSource.Play();
        optionCanvas.SetActive(true);
        optionCanvas_textUserName.text = Progress.instance._playerInfo.UserName;
    }
    public void optionCanvas_CloseButton()
    {
        buttonClickAudioSource.Play();

        if (optionCanvas_textUserName.text.Length > 0)
        {
            if (optionCanvas_textUserName.text != Progress.instance._playerInfo.UserName)
            {
                Progress.instance.SaveUserName(optionCanvas_textUserName.text);
            }
        }

        optionCanvas.SetActive(false);
    }

    public void optionCanvas_MiscOnOffButton()
    {
        buttonClickAudioSource.Play();

        GameMute = !GameMute;
        if (GameMute)
        {
            optionCanvas_MusicOnOffButton.sprite = optionCanvas_MusicOffSprint;
            BackMusic.mute = true;
        }
        else
        {
            BackMusic.mute = false;
            optionCanvas_MusicOnOffButton.sprite = optionCanvas_MusicOnSprint;
        }
    }


    public void Btn_TopScore()
    {
        buttonClickAudioSource.Play();

        topScoreCanvas.SetActive(true);
    }
    public void Btn_CloseSection()
    {
        buttonClickAudioSource.Play();

        topScoreCanvas.SetActive(false);
    }
    public void Contact()
    {
        try
        {
            string url = "";
            switch (PulishFor.Pulish)
            {
                case Pulish.Playe:
                    url = "market://details?id=" + Application.identifier;
                    break;
                case Pulish.Myket:
                    url = "myket://comment?id=" + Application.identifier;
                    break;
                case Pulish.Bazar:
                    url = "bazaar://details?id=" + Application.identifier;
                    break;
                default:
                    break;
            }

            Application.OpenURL(url);

        }
        catch (Exception ex)
        {

            //lblerror.text = ex.Message;
        }



    }



}
