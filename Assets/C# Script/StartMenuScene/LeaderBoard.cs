using Assets.C__Script.GameCore.Api;
using Assets.C__Script.GameCore.Api.Models;
using Assets.C__Script.StartMenuScene;
using Assets.DataLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] GameObject rowItemPrefabs;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject content;
    [SerializeField] GameObject waitingNetwork;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AfterServiceInit());
    }

    IEnumerator AfterServiceInit()
    {
        waitingNetwork.SetActive(true);
        CleanContent();

        if (!ApplicationServices.ServiceIsReady)
            yield return new WaitUntil(() => !ApplicationServices.ServiceIsReady);


        if (!BinoGameServiceApi_LeaderBoard.leaderBoardIsRady)
        {
            Debug.Log($"BinoGameServiceApi_LeaderBoard.leaderBoardIsRady : {BinoGameServiceApi_LeaderBoard.leaderBoardIsRady}");
            yield return new WaitUntil(() => BinoGameServiceApi_LeaderBoard.leaderBoardIsRady);
            Debug.Log($"BinoGameServiceApi_LeaderBoard.leaderBoardIsRady : {BinoGameServiceApi_LeaderBoard.leaderBoardIsRady}");
            RefreshData();
        }
        else
        {
            RefreshData();
        }

    }

    ScrollRect scrollRect;
    RectTransform contentPanel;
    private GameObject PlayerRow;

    public void SnapTo(RectTransform target)
    {
        scrollRect = panel.GetComponent<ScrollRect>();
        contentPanel = content.GetComponent<RectTransform>();

        Canvas.ForceUpdateCanvases();

        contentPanel.anchoredPosition =
                (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
                - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);


        waitingNetwork.SetActive(false);
    }

    public void RefreshData()
    {

        var player = ApplicationServices.playerInfoService.GetPlayerInfo();
        if (player.Setting.Api_LeaderBordData != null)
        {
            var api_LeaderBoard = JsonUtility.FromJson<Api_LeaderBoardList>("{\"dataArray\":" + player.Setting.Api_LeaderBordData + "}");

            PlayerRow = null;
            foreach (var item in api_LeaderBoard.dataArray)
            {
                bool ThisMe = player.ServerPlayerId == item.userId;
                var rowItem = Instantiate(rowItemPrefabs, content.transform);
                rowItem.GetComponent<RowItem>().Init($"#{item.index}", item.sysName, $"{item.score} m", ThisMe);
                if (ThisMe)
                    PlayerRow = rowItem;
            }
            StartCoroutine(ScrollToMe());
        }

    }

    private void CleanContent()
    {
        try
        {
            for (var i = content.transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(content.transform.GetChild(i).gameObject);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            ///throw;
        }
    }

    IEnumerator ScrollToMe()
    {
        yield return new WaitForEndOfFrame();
        if (PlayerRow != null)
        {
            SnapTo(PlayerRow.GetComponent<RectTransform>());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
