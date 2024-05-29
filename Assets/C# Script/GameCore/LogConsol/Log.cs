using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Log : MonoBehaviour
{
    static List<string> lstLogs = new List<string>();
    [SerializeField]
    static UnityEngine.UI.Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
        DontDestroyOnLoad(this);
        ShowLogs();
    }
    public static void Add(string t)
    {
        lstLogs.Add($"\n-{DateTime.Now.Minute}:{DateTime.Now.Millisecond.ToString("00")}-> {t}");
        ShowLogs();
        Debug.Log(t);
    }

    public void removeLogFromTop()
    {
        lstLogs.RemoveAt(0);
        ShowLogs();
    }
    private static void ShowLogs()
    {
        if (text != null)
        {
            text.text = string.Join(null, lstLogs);
        }
    }
}

public enum logLevel
{
    Info, Debug, Error, Develop
}
