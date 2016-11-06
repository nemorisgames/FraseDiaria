using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PreLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("QuoteCheck", -1);
		SceneManager.LoadScene ("Quote_RC");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
