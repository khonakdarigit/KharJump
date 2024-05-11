using Assets.C__Script.Models;
using Assets.C__Script.StartMenuScene;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class API : MonoBehaviour
{
    [SerializeField] private string gameID;


    public static API instance;

    private PlayerInfoApi playerInfoApi;

    public string ApiWebsite;

    public List<TopScoreViewModel> _topScoreAll;
    public List<TopScoreViewModel> _topScoreWeek;
    public List<TopScoreViewModel> _topScoreDay;


    // Start is called before the first frame update
    void Start()
    {
        //ApiWebsite = "https://localhost:44328/";
        ApiWebsite = "https://hidein.herokuapp.com/";

        if (instance == null)
        {
            instance = this;
        }
        //DontDestroyOnLoad(gameObject);

        GetTopScore();
        CheckAddUser();
    }

    internal void AddNewScore(int record)
    {
        try
        {

            if (Progress.instance._playerInfo.API_DB_userID == null)
            {
                AddUser();
            }

            GameScoreApi gamescore = new GameScoreApi() { date = System.DateTime.Now, id = "", gameuserid = Progress.instance._playerInfo.API_DB_userID, score = record };
            string jsonString = JsonUtility.ToJson(gamescore);

            StartCoroutine(request_AddGameScore(string.Format("{0}api/Game/PostScore", ApiWebsite), jsonString));
        }
        catch (Exception ex)
        {
            Debug.Log("Request Erorr: " + ex.Message);
        }



    }
    internal void SaveUserName(string text)
    {
        try
        {

            if (Progress.instance._playerInfo.API_DB_userID == null)
            {
                var playerInfoApi = new PlayerInfoApi() { gameId = gameID, name = Progress.instance._playerInfo.UserName };
                string jsonString = JsonUtility.ToJson(playerInfoApi);

                StartCoroutine(request_AddPlayerUser(string.Format("{0}api/Game/PostUser", ApiWebsite), jsonString));
            }
            else
            {
                StartCoroutine(request_UpdatePlayerUser(string.Format("{0}api/Game/UpdateUser/{1}", ApiWebsite, Progress.instance._playerInfo.API_DB_userID)));
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Request Erorr: " + ex.Message);
        }


    }
    internal void CheckAddUser()
    {
        try
        {
            if (Progress.instance._playerInfo.API_DB_userID == null)
            {
                AddUser();
            }
            else
            {
                StartCoroutine(request_GetPlayerUser(string.Format("{0}api/Game/GetUser/{1}", ApiWebsite, Progress.instance._playerInfo.API_DB_userID)));
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Request Erorr: " + ex.Message);
        }




    }
    private void AddUser()
    {
        try
        {

            PlayerInfoApi playerInfoApi = new PlayerInfoApi() { gameId = gameID, name = Progress.instance._playerInfo.UserName };
            string jsonString = JsonUtility.ToJson(playerInfoApi);

            StartCoroutine(request_AddPlayerUser(string.Format("{0}api/Game/PostUser", ApiWebsite), jsonString));
        }
        catch (Exception ex)
        {
            Debug.Log("Request Erorr: " + ex.Message);
        }


    }
    internal void GetTopScore()
    {
        try
        {
            string url;

            // All 100
            url = string.Format("{0}api/Game/GetTopScore?gameId={1}&allWeekDay={2}&take={3}", ApiWebsite, gameID, "100", 30);
            StartCoroutine(request_GetTopScoreAll(url));
            // Week 010
            url = string.Format("{0}api/Game/GetTopScore?gameId={1}&allWeekDay={2}&take={3}", ApiWebsite, gameID, "010", 30);
            StartCoroutine(request_GetTopScoreWeek(url));
            // Day 001
            url = string.Format("{0}api/Game/GetTopScore?gameId={1}&allWeekDay={2}&take={3}", ApiWebsite, gameID, "001", 30);
            StartCoroutine(request_GetTopScoreDay(url));
        }
        catch (Exception ex)
        {
            Debug.Log("Request Erorr: " + ex.Message);
        }



    }
    IEnumerator request_GetPlayerUser(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            playerInfoApi = JsonUtility.FromJson<PlayerInfoApi>(uwr.downloadHandler.text);
            if (playerInfoApi.name != Progress.instance._playerInfo.UserName)
            {
                StartCoroutine(request_UpdatePlayerUser(string.Format("{0}api/Game/UpdateUser/{1}", ApiWebsite, Progress.instance._playerInfo.API_DB_userID)));
            }
        }
    }
    IEnumerator request_UpdatePlayerUser(string url)
    {

        string jsonString = JsonUtility.ToJson(
            new PlayerInfoApi()
            {
                id = Progress.instance._playerInfo.API_DB_userID,
                gameId = gameID,
                name = Progress.instance._playerInfo.UserName
            });


        var uwr = new UnityWebRequest(url, "PUT");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonString);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received UpdatePlayerUser : " + uwr.downloadHandler.text);
        }
    }
    IEnumerator request_AddPlayerUser(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);

            var playerInfoApi = JsonUtility.FromJson<PlayerInfoApi>(uwr.downloadHandler.text);

            if (playerInfoApi != null && playerInfoApi.id.Length > 5)
            {
                Progress.instance.Save_API_DB_userID(playerInfoApi.id);
                Debug.Log("Save API user id : " + playerInfoApi.id);
            }
        }
    }
    IEnumerator request_AddGameScore(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);

            var gamescore = JsonUtility.FromJson<GameScoreApi>(uwr.downloadHandler.text);

        }
    }
    IEnumerator request_GetTopScoreAll(string uri)
    {

        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            var list = JsonUtility.FromJson<TopScoreList>(uwr.downloadHandler.text);
            _topScoreAll = list.topScoreList;


        }
    }
    IEnumerator request_GetTopScoreWeek(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            var list = JsonUtility.FromJson<TopScoreList>(uwr.downloadHandler.text);
            _topScoreWeek = list.topScoreList;

        }
    }
    IEnumerator request_GetTopScoreDay(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            var list = JsonUtility.FromJson<TopScoreList>(uwr.downloadHandler.text);
            _topScoreDay = list.topScoreList;

        }
    }
}
