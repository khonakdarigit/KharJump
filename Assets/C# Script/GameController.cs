using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;
using UnityEngine.SceneManagement;
using System;
using Assets;
using Assets.Script;

public class GameController : Singleton<GameController>
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

    [SerializeField] AudioClip audioClip_Click;

    public void ButtonClickSound()
    {
        PlaySound(audioClip_Click);
    }

    public void PlaySound(AudioClip audioClip)
    {
        AudioSource source_ = gameObject.AddComponent<AudioSource>();
        Destroy(source_, 3);
        source_.clip = audioClip;
        source_.volume = 1f;
        source_.Play();
    }

    public void PlayButtonClick()
    {
        ButtonClickSound();

        SceneManager.LoadScene(1);
    }

    //public void OptionButtonClick()
    //{
    //    ButtonClickSound();
    //    optionCanvas.SetActive(true);
    //    optionCanvas_textUserName.text = Progress.Instance._playerInfo.UserName;
    //}
    //public void optionCanvas_CloseButton()
    //{
    //    ButtonClickSound();
    //    if (optionCanvas_textUserName.text.Length > 0)
    //    {
    //        if (optionCanvas_textUserName.text != Progress.Instance._playerInfo.UserName)
    //        {
    //            Progress.Instance.SaveUserName(optionCanvas_textUserName.text);
    //        }
    //    }

    //    optionCanvas.SetActive(false);
    //}

    public void optionCanvas_MiscOnOffButton()
    {
        ButtonClickSound();

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
        ButtonClickSound();

        topScoreCanvas.SetActive(true);
    }
    public void Btn_CloseSection()
    {
        ButtonClickSound();


        topScoreCanvas.SetActive(false);
    }



}
