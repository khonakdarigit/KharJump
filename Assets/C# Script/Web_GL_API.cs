//using Assets.C__Script.Models;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Networking;

//public class Web_GL_API : MonoBehaviour
//{
//    public static Web_GL_API instance;

//    private PlayerInfoApi playerInfoApi;

//    public string ApiWebsite;

//    public List<TopScoreViewModel> _topScoreAll;
//    public List<TopScoreViewModel> _topScoreWeek;
//    public List<TopScoreViewModel> _topScoreDay;
//    public string gameID;

//    public TMPro.TextMeshProUGUI TextMeshProUGUI;
//    // Start is called before the first frame update
//    void Start()
//    {
//        //ApiWebsite = "https://localhost:44328/";
//        ApiWebsite = "https://hidein.herokuapp.com/";

//        if (instance == null)
//        {
//            instance = this;
//        }
//        //DontDestroyOnLoad(gameObject);

//        GetTopScore();
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }


//    internal void GetTopScore()
//    {
//        try
//        {
//            string url;

//            // All 100
//            url = string.Format("{0}api/Game/GetTopScore?gameId={1}&allWeekDay={2}&take={3}", ApiWebsite, gameID, "100", 30);
//            StartCoroutine(request_GetTopScoreAll(url));
//        }
//        catch (Exception ex)
//        {
//            Debug.Log("Request Erorr: " + ex.Message);
//        }

//    }

//    IEnumerator request_GetTopScoreAll(string uri)
//    {

//        UnityWebRequest uwr = UnityWebRequest.Get(uri);
//        uwr.SetRequestHeader("Access-Control-Allow-Credentials", "true");
//        uwr.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
//        uwr.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
//        uwr.SetRequestHeader("Access-Control-Allow-Origin", "*");
//        //uwr.SetRequestHeader("Content-Type", "application/json");

//        //uwr.SetRequestHeader("Access-Control-Allow-Credentials", "true");

//        yield return uwr.SendWebRequest();

//        if (uwr.isNetworkError)
//        {
//            Debug.Log("Error While Sending: " + uwr.error);
//        }
//        else
//        {
//            Debug.Log("Received: " + uwr.downloadHandler.text);
//            TextMeshProUGUI.text = uwr.downloadHandler.text;
//            var list = JsonUtility.FromJson<TopScoreList>(uwr.downloadHandler.text);
//            _topScoreAll = list.topScoreList;


//        }
//    }
//}
