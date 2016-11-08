using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PreLoad : MonoBehaviour {
	private int _disclaimer;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetInt ("QuoteCheck", -1);
		_disclaimer = PlayerPrefs.GetInt ("Disclaimer", 1);
		if (_disclaimer == 1) {
			SceneManager.LoadScene ("Disclaimer_SL");
		} else {
			SceneManager.LoadScene ("Quote_RC");
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
