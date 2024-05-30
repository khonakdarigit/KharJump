using UnityEngine;
using System.Collections;
using ArabicSupport;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class FixArabic3DText : MonoBehaviour
{

    public bool showTashkeel = false;
    public bool useHinduNumbers = false;
    private string _text;

    // Use this for initialization
    void Start()
    {
        //Text textMesh = gameObject.GetComponent<UnityEngine.UI.Text>();


        //string fixedText = ArabicFixer.Fix(textMesh.text, showTashkeel, useHinduNumbers);

        //gameObject.GetComponent<Text>().text = fixedText;
    }

    public void SetText(string text)
    {

        Text textMesh = gameObject.GetComponent<UnityEngine.UI.Text>();
        textMesh.text = text;
        if (StringIsPersian(text))
            textMesh.font = Resources.Load<Font>("Fonts/segoeui");
        string fixedText = ArabicFixer.Fix(textMesh.text, showTashkeel, useHinduNumbers);

        gameObject.GetComponent<Text>().text = fixedText;
    }


    public static bool StringIsPersian(string str)
    {
        Regex regex = new Regex("[\u0600-\u06ff]|[\u0750-\u077f]|[\ufb50-\ufc3f]|[\ufe70-\ufefc]");
        bool isPersian = regex.IsMatch(str);
        return isPersian;
    }

}
