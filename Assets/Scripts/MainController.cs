using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {
	
	public I2.Loc.Localize qLocalize;
	public I2.Loc.Localize aLocalize;
	public I2.Loc.Localize dLocalize;
	// Use this for initialization
	void Start () {
		ChangeQuote ("Quote2");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			ChangeQuote ("Quote1");
			//ChangeLanguage ("Spanish");

			Debug.Log ("Cita 1, en españolo");
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			ChangeQuote ("Quote2");
			//ChangeLanguage ("English");

			Debug.Log ("Cita 2, en inglish");
		}
	}

	void ChangeQuote(string quote){
		qLocalize.SetTerm (quote);
		aLocalize.SetTerm (quote);
		dLocalize.SetTerm (quote);
	}

	void ChangeLanguage(string language){
		if( I2.Loc.LocalizationManager.HasLanguage(language))
			I2.Loc.LocalizationManager.CurrentLanguage = language;
	}
}
