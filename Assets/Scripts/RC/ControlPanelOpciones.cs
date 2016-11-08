using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlPanelOpciones : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public void irQuote(){
		PlayerPrefs.SetInt ("QuoteCheck", -1);
		SceneManager.LoadScene ("Quote_RC");
	}

	public void irHistory(){
		SceneManager.LoadScene ("History_RC");
	}
		
	public void irAbout(){
		SceneManager.LoadScene ("About_SL");
	}

	public void okDisclaimer() {
		PlayerPrefs.SetInt ("Disclaimer", -1);
		irQuote ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
