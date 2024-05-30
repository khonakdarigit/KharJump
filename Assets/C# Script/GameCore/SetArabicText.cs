using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ArabicSupport;

public class SetArabicText : MonoBehaviour {

	[TextArea]
	public string text;

	public bool ShowTashkeel = false;
	public bool UseHinduNumbers = true;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<TextMesh>().text = ArabicFixer.Fix(text);
		//gameObject.guiText.fontSize = 60;
		Debug.Log(ArabicFixer.Fix("نص عربي على يونيتي، أهلا وسهلا", ShowTashkeel, UseHinduNumbers));
	}

}
