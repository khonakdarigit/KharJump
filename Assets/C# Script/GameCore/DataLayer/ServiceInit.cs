using Assets.DataLayer.Infrastructure.Services;
using Assets.DataLayer.Infrastructure;
using Assets.DataLayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class ServiceInit : Singleton<ServiceInit>
{
    // Start is called before the first frame update
    void Start()
    {
        // Game Service
        ApplicationServices.dbService = new ApplicationDbService();
        ApplicationServices.playerInfoService = new PlayerInfoService(ApplicationServices.dbService);

        ApplicationServices.ServiceIsReady = true;

        Debug.Log($"" +
            $"Player ID : {ApplicationServices.playerInfoService.GetPlayerInfo()._id} ");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
