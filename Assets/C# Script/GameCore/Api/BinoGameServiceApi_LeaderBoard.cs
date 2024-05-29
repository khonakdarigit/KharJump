using Assets.C__Script.GameCore.Api.Models;
using Assets.DataLayer;
using Assets.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

namespace Assets.C__Script.GameCore.Api
{
    public class BinoGameServiceApi_LeaderBoard : Singleton<BinoGameServiceApi_LeaderBoard>
    {
        private string apiUrl = BinoGameServiceApi.apiUrl;

        private void Start()
        {
            StartCoroutine(AfterTokenReady());
        }

        IEnumerator AfterTokenReady()
        {
            Debug.Log($"BinoGameServiceApi.tokenIsReady before {BinoGameServiceApi.tokenIsReady}");

            if (!BinoGameServiceApi.tokenIsReady)
                yield return new WaitUntil(() => BinoGameServiceApi.tokenIsReady);

            Debug.Log($"BinoGameServiceApi.tokenIsReady after {BinoGameServiceApi.tokenIsReady}");
            Api_AddScoreToLeaderBord();
        }

        public void Api_AddScoreToLeaderBord()
        {
            if (BinoGameServiceApi.tokenIsReady)
                try
                {
                    var player = ApplicationServices.playerInfoService.GetPlayerInfo();

                    Api_LeaderBoard api_LeaderBoard = new Api_LeaderBoard()
                    {
                        userId = player.ServerPlayerId,
                        sysName = player.SysName,
                        score = player.Record
                    };
                    string jsonString = JsonUtility.ToJson(api_LeaderBoard);

                    var url = $"{apiUrl}api/Leaderboard";

                    var uwr = new UnityWebRequest(url, "POST");
                    byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonString);
                    uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
                    uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();


                    StartCoroutine(BinoGameServiceApi.Instance.WebRequest(uwr,
                        delegate (string res)
                    {


                    },
                        delegate (string failMsg)
                    {
                        Debug.Log($"request {url} : failMsg{failMsg}");
                    }, true));
                }
                catch (Exception ex)
                {
                    Debug.Log("Api_AddScoreToLeaderBord Request Error: " + ex.Message);
                }
        }

    }
}
