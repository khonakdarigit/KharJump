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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AfterServiceInit());
    }

    IEnumerator AfterServiceInit()
    {
        if (!ApplicationServices.ServiceIsReady)
            yield return new WaitUntil(() => !ApplicationServices.ServiceIsReady);

        if (ApplicationServices.playerInfoService.GetPlayerInfo().Setting.Api_LeaderBordData != null)
            RefreshData();
        else
        {
            if (!BinoGameServiceApi_LeaderBoard.leaderBoardIsRady)
            {
                yield return new WaitUntil(() => BinoGameServiceApi_LeaderBoard.leaderBoardIsRady);
                RefreshData();
            }

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
    }

    public void RefreshData()
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


        var player = ApplicationServices.playerInfoService.GetPlayerInfo();
        if (player.Setting.Api_LeaderBordData != null)
        {
            var api_LeaderBoard = JsonUtility.FromJson<Api_LeaderBoardList>("{\"dataArray\":" + player.Setting.Api_LeaderBordData + "}");

            int index = 0;
            PlayerRow = null;
            foreach (var item in api_LeaderBoard.dataArray)
            {
                bool ThisMe = player.ServerPlayerId == item.userId;
                var rowItem = Instantiate(rowItemPrefabs, content.transform);
                rowItem.GetComponent<RowItem>().Init($"#{++index}", item.sysName, $"{item.score} m", ThisMe);
                if (ThisMe)
                    PlayerRow = rowItem;
            }
            StartCoroutine(ScrollToMe());
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
