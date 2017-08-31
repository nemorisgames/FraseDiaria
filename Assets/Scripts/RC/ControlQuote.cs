using UnityEngine;
using System.Collections;
using System;

public class ControlQuote : MonoBehaviour {

	public UILabel fraseLabel;
	public UILabel autorLabel;
	public UILabel fechaLabel;
	public UILabel fechaSistLabel;
	public UITexture imagenFondo;
	public Texture[] imagenes;
	System.DateTime startDay;
	int lastDay;
	public float fontScale = 5f;

	// Use this for initialization
	void Start () {
		imagenFondo.alpha = 1;
		cargarFrase ();

		if (PlayerPrefs.HasKey ("StartDay")) {
			startDay = System.DateTime.Parse ("1/1/2016 10:32:21 PM").Date;//PlayerPrefs.GetString ("StartDay")); //
		} else {
			startDay = System.DateTime.Parse ("1/1/2016 10:32:21 PM").Date;
			PlayerPrefs.SetString ("StartDay", startDay.ToString());
		}

		if(fechaSistLabel != null)
			fechaSistLabel.text = FechaSistema();
		lastDay = System.DateTime.Now.Day;

		
	}

	public void cargarFrase(){
		StartCoroutine (cargarFraseRutina ());
	}
	IEnumerator cargarFraseRutina(){
		yield return new WaitForEndOfFrame ();
		if (PlayerPrefs.GetInt ("QuoteCheck", -1) >= 0) {
			inicializar (PlayerPrefs.GetInt ("QuoteCheck", -1));
		} else {
			inicializar ((System.DateTime.Now.Date - startDay.Date).Days % 365);
		}
		fraseLabel.fontSize = PlayerPrefs.GetInt("FontSize",80);
		float i = fraseLabel.GetComponent<UIWidget>().localSize.y;
		while(i > 1200){
			FontSizeDownTemp();
			i = fraseLabel.GetComponent<UIWidget>().localSize.y;
		}
	}

	public void inicializar(int indice){
		imagenFondo.mainTexture = imagenes [indice % imagenes.Length];
		fraseLabel.GetComponent<I2.Loc.Localize>().SetTerm ("Quote" + (indice + 1));
		string completo = I2.Loc.ScriptLocalization.Get("Quote" + (indice + 1));
		string[] partes = completo.Split ('|');
		print ("mostrando " + indice);
		if (partes.Length > 1) {
			fraseLabel.text = partes [0];
			autorLabel.text = partes [1];
			if (!partes [2].Contains ("/") && !partes [2].Contains ("-")) {
				int dias = -1;
				if (int.TryParse (partes [2], out dias)) {
					fechaLabel.text = System.DateTime.Parse ("01/01/1900").AddDays (dias).Date.ToShortDateString ();
				} else {
					fechaLabel.text = "-";
				}
			} else
				fechaLabel.text = partes [2];
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*if(lastDay != System.DateTime.Now.Day && fechaSistLabel != null)
			fechaSistLabel.text = FechaSistema();*/
			Debug.Log(fraseLabel.GetComponent<UIWidget>().localSize.y);
	}

	string FechaSistema(){
		string dia = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(System.DateTime.Now.ToString("dddd"));
		string mes = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(System.DateTime.Now.ToString("MMMM"));
		string s = dia + "\n" + mes + " " + System.DateTime.Now.ToString("dd, yyyy");
		return s;
	}

	void FontSize(int n, bool b){
		fraseLabel.fontSize += (int)(fontScale*Mathf.Sign(n));
		fechaLabel.fontSize += (int)(fontScale*Mathf.Sign(n));
		fraseLabel.fontSize = Mathf.Clamp(fraseLabel.fontSize,60,100);
		fechaLabel.fontSize = Mathf.Clamp(fraseLabel.fontSize,60,100);
		if(b)
			PlayerPrefs.SetInt("FontSize",fraseLabel.fontSize);
	}

	public void FontSizeUp(){
		if(fraseLabel.GetComponent<UIWidget>().localSize.y <= 1200)
			FontSize(1, true);
	}

	public void FontSizeDown(){
		if(fraseLabel.GetComponent<UIWidget>().localSize.y >= 500)
			FontSize(-1, true);
	}

	void FontSizeDownTemp(){
		if(fraseLabel.GetComponent<UIWidget>().localSize.y >= 500)
			FontSize(-1, false);
	}
}
