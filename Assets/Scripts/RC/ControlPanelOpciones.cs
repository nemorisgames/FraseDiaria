using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlPanelOpciones : MonoBehaviour {

	public UIToggle [] colors;
	public UITexture imagenFondo;
	public UISprite background;
	public Texture [] imagenes;
	public string [] colorsG1;
	public string [] colorsG2;
	public string bgTextLight;
	public string bgTextDark;
	public bool light = true;

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
		if(PlayerPrefs.GetInt("BGColor",0) == 4)
			light = false;
		else
			light = true;
		if(imagenes.Length != 0)
			imagenFondo.mainTexture = imagenes[PlayerPrefs.GetInt("BGColor",0)];
		else if(colorsG1.Length != 0){
			Color c1;
			ColorUtility.TryParseHtmlString(colorsG1[PlayerPrefs.GetInt("BGColor",0)], out c1);
			Color c2;
			ColorUtility.TryParseHtmlString(colorsG2[PlayerPrefs.GetInt("BGColor",0)], out c2);
			background.applyGradient = true;
			background.gradientTop = c1;
			background.gradientBottom = c2;
			/*if(PlayerPrefs.GetInt("BGColor",0) == 4){
				SetTextButtonColor(1);
			}
			else{
				SetTextButtonColor(0);
			}*/
				
		}
	}

	void SetTextButtonColor(int i){
		GameObject [] objects = GameObject.FindGameObjectsWithTag("SwitchColor");
		Color [] recolor = new Color[2];
		ColorUtility.TryParseHtmlString(bgTextLight, out recolor[0]);
		ColorUtility.TryParseHtmlString(bgTextDark, out recolor[1]);

		for(int j = 0;j<objects.Length;j++){
			UIButton btn = objects[j].GetComponent<UIButton>();
			if(btn != null){
				btn.hover = (i == 0 ? recolor[1] : recolor [0]);
				btn.pressed = (i == 0 ? recolor[1] : recolor [0]);
				btn.defaultColor = (i == 0 ? recolor[0] : recolor [1]);
			}
			else{
				UISprite spr = objects[j].GetComponent<UISprite>();
				if(spr != null){
					spr.color = (i == 0 ? recolor[0] : recolor [1]);
				}
			}
		}

		UILabel [] labels = GameObject.FindObjectsOfType<UILabel>();
		for(int j = 0;j<labels.Length;j++){
			labels[j].color = (i == 0 ? recolor[0] : recolor [1]);
		}

		if(i == 0)
			light = true;
		else
			light = false;
	}

	public Color GetColor(int i){
		Color [] recolor = new Color[2];
		ColorUtility.TryParseHtmlString(bgTextLight, out recolor[0]);
		ColorUtility.TryParseHtmlString(bgTextDark, out recolor[1]);
		return recolor[i];
	}


}
