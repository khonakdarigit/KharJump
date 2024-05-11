using Realms;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    private Realm _realm;

    public PlayerInfo _playerInfo;
    public int Record { get; set; }

    public static Progress instance;

    private void Start()
    {
        if (instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    void OnEnable()
    {

        var config = new RealmConfiguration { SchemaVersion = 1 };

        _realm = Realm.GetInstance(config);

        //var Config = _realm.Config;
        //_realm.Dispose();
        //Realm.DeleteRealm(_realm.Config);
        //_realm = Realm.GetInstance();

        _playerInfo = _realm.Find<PlayerInfo>("DonkeyJump");
        if (_playerInfo == null)
        {
            Debug.Log("Database is empty");
            _realm.Write(() =>
            {
                _playerInfo = _realm.Add(new PlayerInfo() { UserName = "Player_" + UnityEngine.Random.Range(1000, 9999).ToString(), gamerTag = "DonkeyJump" });
            });
            Debug.Log(string.Format("Write {0} _playerInfo in DB", _playerInfo.gamerTag));
        }
        else
        {
            Debug.Log("Database is ready _playerInfo Saved : " + _playerInfo.gamerTag);
        }

        Record = _playerInfo.Record;
    }

    void OnDisable()
    {
        _realm.Dispose();
    }

    internal void SaveUserName(string text)
    {
        _realm.Write(() =>
        {
            _playerInfo.UserName = text;
        });

        //API.instance.SaveUserName(text);
    }
    internal void Save_API_DB_userID(string API_DB_userID)
    {
        _realm.Write(() =>
        {
            _playerInfo.API_DB_userID = API_DB_userID;
        });
    }
    public void Save(int record)
    {

        if (record > Convert.ToInt32(_playerInfo.Record))
        {
            _realm.Write(() =>
            {
                _playerInfo.Record = record;
            });

        }

        //API.instance.AddNewScore(App.instance.liveScore);
    }
    public void SetPlayerInfo(string value)
    {
        _playerInfo = JsonUtility.FromJson<PlayerInfo>(value);
    }

}
