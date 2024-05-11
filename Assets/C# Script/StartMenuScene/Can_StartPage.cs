using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Can_StartPage : MonoBehaviour
{
    public UnityEngine.UI.Text txtScore;
    public UnityEngine.UI.Button btn_Play_obg;
    public UnityEngine.UI.Text txt_Version;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        txtScore.text = string.Format("Your Score :\n{0} m", Progress.instance._playerInfo.Record);

        txt_Version.text = "Version : " + Application.version.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void btn_Play()
    {
        StartCoroutine(App.instance.ExecuteAfterTime(0.2f, () =>
        {
            SceneManager.LoadScene(1);
        }));

    }

    public void btn_Exit()
    {
        StartCoroutine(App.instance.ExecuteAfterTime(0.2f, () =>
        {
            Application.Quit();
        }));
    }
}
