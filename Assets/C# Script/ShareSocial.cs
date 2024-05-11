using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareSocial : MonoBehaviour
{

    string subject;

    string body;

    public void OnAndroidTextSharingClick()
    {
        subject = "Donkey Jump Play Now!";
        string link = "";

        switch (PulishFor.Pulish)
        {
            case Pulish.Playe:
                link = "https://play.google.com/store/apps/details?id=" + Application.identifier;
                break;
            case Pulish.Myket:
                link = "https://myket.ir/app/" + Application.identifier;
                break;
            case Pulish.Bazar:
                link = "https://cafebazaar.ir/app/" + Application.identifier;
                break;
            default:
                break;
        }


        body = string.Format(
            "\nI reached to {0} m in action hero. Check out how far you go: \n" + link,
            Progress.instance.Record);

        StartCoroutine(ShareAndroidText());
    }


    IEnumerator ShareAndroidText()
    {
        yield return new WaitForEndOfFrame();
        //execute the below lines if being run on a Android device
        //Reference of AndroidJavaClass class for intent
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        //Reference of AndroidJavaObject class for intent
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        //call setAction method of the Intent object created
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        //set the type of sharing that is happening
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        //add data to be passed to the other activity i.e., the data to be sent
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "TITLE");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), subject + body);
        //get the current activity
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        //start the activity by sending the intent data
        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
        currentActivity.Call("startActivity", jChooser);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
