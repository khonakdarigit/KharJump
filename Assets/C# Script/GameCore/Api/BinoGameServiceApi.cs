using Assets.C__Script.GameCore.Api.Models;
using Assets.DataLayer;
using Assets.DataLayer.Domain.Models;
using Assets.Script;
using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.C__Script.GameCore.Api
{
    public class BinoGameServiceApi : Singleton<BinoGameServiceApi>
    {
        private string _gameName;
        public static string apiUrl = "https://localhost:7208/";
        private string token;
        private string _apiPass = "*A*A*A*LoginPass*A*A*A*A";
        public static bool tokenIsReady = false;
        public void Start()
        {
            _gameName = Application.identifier;

            StartCoroutine(AfterServiceInit());
        }

        IEnumerator AfterServiceInit()
        {
            if (!ApplicationServices.ServiceIsReady)
                yield return new WaitUntil(() => !ApplicationServices.ServiceIsReady);

            CheckRegisterAndFillToken();
        }

        public IEnumerator WebRequest(UnityWebRequest unityWeb, Action<string> OnSuccess, Action<string> OnFail = null, bool AuthorizationRequest = false)
        {
            unityWeb.SetRequestHeader("accept", "*/*");
            unityWeb.SetRequestHeader("Content-Type", "application/json");

            if (AuthorizationRequest)
                unityWeb.SetRequestHeader("Authorization", $"Bearer {token}");

            Debug.Log($"unityWeb Sending: {unityWeb.url}");
            yield return unityWeb.SendWebRequest();

            if (unityWeb.isNetworkError)
            {
                Debug.Log($"Error While Sending: {unityWeb.error}");
            }
            else
            {
                Debug.Log($"unityWeb responseCode: {unityWeb.responseCode}");

                if (unityWeb.responseCode >= 200 && unityWeb.responseCode <= 204)
                {
                    Debug.Log("Received: " + unityWeb.downloadHandler.text);
                    OnSuccess.Invoke(unityWeb.downloadHandler.text);
                }
                else if (unityWeb.responseCode == 401)
                {
                    Debug.Log("Get refresh token ...");
                    if (AuthorizationRequest == true)
                        Api_RefreshToken();

                    OnFail?.Invoke(unityWeb.downloadHandler.text);
                }
                else
                {
                    OnFail?.Invoke(unityWeb.downloadHandler.text);
                }

            }
        }

        public bool CheckRegisterAndFillToken()
        {
            var player = ApplicationServices.playerInfoService.GetPlayerInfo();
            if (player.ServerPlayerId == null)
            {
                Api_Register();
                return false;
            }
            else
            {
                Api_RefreshToken();
                return true;
            }
        }
        public void Api_Register()
        {
            try
            {
                var player = ApplicationServices.playerInfoService.GetPlayerInfo();

                Api.Models.Api_PlayerInfo api_Login = new Api.Models.Api_PlayerInfo()
                {
                    devicePlayerId = player._id.ToString(),
                    gameIdTag = _gameName,
                    sysName = player.SysName,
                    username = player._id.ToString(),
                    password = _apiPass,
                };

                string jsonString = JsonUtility.ToJson(api_Login);

                var url = $"{apiUrl}api/Auth/register";

                var uwr = new UnityWebRequest(url, "POST");
                byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonString);
                uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
                uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();


                StartCoroutine(WebRequest(uwr, delegate (string res)
                {
                    var registeredPlayer = JsonUtility.FromJson<Api_Login>(uwr.downloadHandler.text);
                    player.ServerPlayerId = registeredPlayer.id;

                    ApplicationServices.playerInfoService.UpdatePlayerInfo(player);

                    Api_Login();
                }));
            }
            catch (Exception ex)
            {
                Debug.Log("Request Error: " + ex.Message);
            }
        }
        public void Api_Login()
        {
            var player = ApplicationServices.playerInfoService.GetPlayerInfo();

            Api.Models.Api_Login api_Login = new Api.Models.Api_Login() { username = player._id.ToString(), password = _apiPass };

            string jsonString = JsonUtility.ToJson(api_Login);

            var url = $"{apiUrl}api/Auth/login";

            var uwr = new UnityWebRequest(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonString);
            uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();


            StartCoroutine(WebRequest(uwr, delegate (string res)
            {
                var api_token = JsonUtility.FromJson<Api_Token>(uwr.downloadHandler.text);

                player.Setting.Api_Token = api_token.token;
                player.Setting.Api_RefreshToken = api_token.refreshToken;

                ApplicationServices.playerInfoService.UpdatePlayerInfo(player);

                token = api_token.token;
                tokenIsReady = true;
                Log.Add($"Register And Login And Add User Successfully Complete player.ServerPlayerId {player.ServerPlayerId} token {token}");

            }));
        }
        public void Api_RefreshToken()
        {
            var player = ApplicationServices.playerInfoService.GetPlayerInfo();

            Api.Models.Api_RefreshToken api_RefreshToken = new Api_RefreshToken() { accessToken = player.Setting.Api_Token, refreshToken = player.Setting.Api_RefreshToken };

            string jsonString = JsonUtility.ToJson(api_RefreshToken);

            var url = $"{apiUrl}api/Auth/refresh-token";

            var uwr = new UnityWebRequest(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonString);
            uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();


            StartCoroutine(WebRequest(uwr, delegate (string res)
            {
                var api_RefreshToken = JsonUtility.FromJson<Api_RefreshToken>(uwr.downloadHandler.text);

                player.Setting.Api_Token = api_RefreshToken.accessToken;
                player.Setting.Api_RefreshToken = api_RefreshToken.refreshToken;

                ApplicationServices.playerInfoService.UpdatePlayerInfo(player);

                token = api_RefreshToken.accessToken;

                tokenIsReady = true;

                Debug.Log($"Get new refresh token successfully and api is ready token {token}");
            }));
        }
    }
}
