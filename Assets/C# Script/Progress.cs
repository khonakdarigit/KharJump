//using Assets.C__Script.Models;
//using Assets.DataLayer;
//using Assets.Script;
//using Realms;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Progress : Singleton<Progress>
//{
//    private Realm _realm;
//    private Assets.DataLayer.Domain.Models.PlayerInfo _playerInfo;

//    //public PlayerInfo _playerInfo;
//    public int Record { get; set; }

//    void OnEnable()
//    {

//        StartCoroutine(AfterServiceInit());
//    }

//    IEnumerator AfterServiceInit()
//    {
//        if(!ApplicationServices.ServiceIsReady)
//            yield return new WaitUntil(()=>!ApplicationServices.ServiceIsReady);

//        _playerInfo = ApplicationServices.playerInfoService.GetPlayerInfo();
//        Record = _playerInfo.Record;
//    }

//    void OnDisable()
//    {
//        _realm.Dispose();
//    }

//    internal void SaveUserName(string text)
//    {
//        _realm.Write(() =>
//        {
//            _playerInfo.UserName = text;
//        });

//        //API.instance.SaveUserName(text);
//    }
//    internal void Save_API_DB_userID(string API_DB_userID)
//    {
//        _realm.Write(() =>
//        {
//            _playerInfo.API_DB_userID = API_DB_userID;
//        });
//    }
//    public void Save(int record)
//    {

//        if (record > Convert.ToInt32(_playerInfo.Record))
//        {
//            _realm.Write(() =>
//            {
//                _playerInfo.Record = record;
//            });

//        }

//        //API.instance.AddNewScore(App.Instance.liveScore);
//    }
//    public void SetPlayerInfo(string value)
//    {
//        _playerInfo = JsonUtility.FromJson<PlayerInfo>(value);
//    }

//}
