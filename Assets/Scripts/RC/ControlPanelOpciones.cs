using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlPanelOpciones : MonoBehaviour {

	public UIToggle [] colors;
	public UITexture imagenFondo;
	public Texture [] imagenes;
	public string [] colorsWeb;

	// Use this for initialization
	void Start () {
		setBGColor();
	}

	public void irQuote(){
		PlayerPrefs.SetInt ("QuoteCheck", -1);
		//SceneManager.LoadScene ("Quote_NJ");
		StartCoroutine(ControlHistory.cargarEscena());
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

	public void selectColor(string name){
		string aux = name.Substring(10,name.Length - 10);
		int i = 0;
		switch(aux){
			case "Blue":
			i = 0;
			break;
			case "DGreen":
			i = 1;
			break;
			case "LGreen":
			i = 2;
			break;
			case "Yellow":
			i = 3;
			break;
			case "Red":
			i = 4;
			break;
			default:
			i = 0;
			break;	
		};
		PlayerPrefs.SetInt("BGColor",i);
		setBGColor();
	}

	void setBGColor(){
		colors[PlayerPrefs.GetInt("BGColor",0)].Set(true);
		if(imagenes.Length != 0)
			imagenFondo.mainTexture = imagenes[PlayerPrefs.GetInt("BGColor",0)];
		else if(colorsWeb.Length != 0){
			Color c;
			ColorUtility.TryParseHtmlString(colorsWeb[PlayerPrefs.GetInt("BGColor",0)], out c);
			imagenFondo.color = c;
		}
	}



}
