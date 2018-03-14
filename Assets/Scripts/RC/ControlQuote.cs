using UnityEngine;
using System.Collections;
using System;
using VoxelBusters.Utility;
using VoxelBusters.NativePlugins;
using System.IO;
using System.Globalization;

public class ControlQuote : MonoBehaviour {

	public UILabel fraseLabel;
	public UILabel fraseLabelSS;
	public UILabel autorLabel;
	public UILabel fechaLabel;
	public UILabel fechaLabelSS;
	public UILabel diaLabel;
	public UILabel mesLabel;
	public UILabel fechaSistLabel;
	public UILabel urlLabel;
	public UITexture imagenFondo;
	public Texture[] imagenes;
	System.DateTime startDay;
	int lastDay;
	public float fontScale = 5f;
	System.Random rnd;
	public Camera SSCam;
	Camera defaultCam;
	public RenderTexture renderTexture;
	public UIPanel panelSS;
	public bool sharing = false;
	// Use this for initialization
	public UILabel debugLabel;
	public UILabel loading;
	
	void Start () {
		rnd = new System.Random();
		//imagenFondo.alpha = 1;
		cargarFrase ();
		//PlayerPrefs.DeleteAll();
		if (PlayerPrefs.HasKey ("StartDay")) {
			//startDay = System.DateTime.Parse ("12/1/2016 0:00:01 AM").Date;//PlayerPrefs.GetString ("StartDay")); //
			startDay = System.DateTime.ParseExact ("12/1/2017 0:00:01 AM","M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture).Date;
		} else {
			//startDay = System.DateTime.Parse ("12/1/2016 0:00:01 AM").Date;
			startDay = System.DateTime.ParseExact ("12/1/2017 0:00:01 AM","M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture).Date;
			PlayerPrefs.SetString ("StartDay", startDay.ToString());
		}

		CargarFechaSistema();
		lastDay = System.DateTimeOffset.Now.Day;

		float f = (float)Screen.width/(float)Screen.height;
		Debug.Log(f);
		if(f <= 0.58f)
			SSCam.orthographicSize = 0.7f;
		else if(f > 0.58f && f < 0.6f)
			SSCam.orthographicSize = 0.72f;
		else if(f >= 0.6f)
			SSCam.orthographicSize = 0.8f;
		
	}

	void CargarFechaSistema(){
		string s = System.DateTime.Now.DayOfWeek.ToString();
		diaLabel.GetComponent<I2.Loc.Localize>().SetTerm(s);
		diaLabel.text = I2.Loc.ScriptLocalization.Get(s);
		int m = System.DateTime.Now.Month;
		mesLabel.GetComponent<I2.Loc.Localize>().SetTerm("M"+m);
		mesLabel.text = I2.Loc.ScriptLocalization.Get("M"+m);
		fechaSistLabel.text = System.DateTime.Now.ToString("dd, yyyy");
	}

	public void cargarFrase(){
		StartCoroutine (cargarFraseRutina ());
	}
	IEnumerator cargarFraseRutina(){
		yield return new WaitForEndOfFrame ();
		if (PlayerPrefs.GetInt ("QuoteCheck", -1) >= 0) {
			inicializar (PlayerPrefs.GetInt ("QuoteCheck", -1));
		} else {
			inicializar ((System.DateTimeOffset.Now.Date - startDay.Date).Days % 396);
		}
		fraseLabel.fontSize = PlayerPrefs.GetInt("FontSize",80);
		fraseLabelSS.text = fraseLabel.text;
		fraseLabelSS.fontSize = 80;
		fechaLabelSS.text = fechaLabel.text;
	}


	public void inicializar(int indice){
		//imagenFondo.mainTexture = imagenes [indice % imagenes.Length];
		//imagenFondo.mainTexture = SeleccionarFondo();
		fraseLabel.GetComponent<I2.Loc.Localize>().SetTerm ((indice + 1).ToString());
		string lang = I2.Loc.LocalizationManager.CurrentLanguage;
		fraseLabel.text = I2.Loc.ScriptLocalization.Get((indice + 1).ToString());
		I2.Loc.LocalizationManager.CurrentLanguage = "Author";
		autorLabel.text = I2.Loc.ScriptLocalization.Get((indice + 1).ToString());
		I2.Loc.LocalizationManager.CurrentLanguage = "Date";
		fechaLabel.text = I2.Loc.ScriptLocalization.Get((indice + 1).ToString());
		I2.Loc.LocalizationManager.CurrentLanguage = lang;
		
		fechaLabel.text = fechaLabel.text.Trim();
		if(fechaLabel.text.Length == 0 || fechaLabel.text.Length > 10 || fechaLabel.text == "The Mind")
			fechaLabel.text = "";

		fraseLabelSS.text = fraseLabel.text;
		//fraseLabelSS.fontSize = fraseLabel.fontSize;
		fechaLabelSS.text = fechaLabel.text;
		//fechaLabelSS.fontSize = fechaLabel.fontSize;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.KeypadPlus))
			FontSizeUp();
		if(Input.GetKeyDown(KeyCode.KeypadMinus))
			FontSizeDown();
	}

	Texture SeleccionarFondo(){
		int indice = PlayerPrefs.GetInt("TipoFondo",0);
		Texture fondo = new Texture();
		switch(indice){
			case 0:
			fondo = imagenes[rnd.Next(0,4)];
			break;
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			fondo = imagenes[indice - 1];
			break;
		}
		return fondo;
	}

	void FontSize(int n, bool b){
		fraseLabel.fontSize += (int)(fontScale*Mathf.Sign(n));
		fechaLabel.fontSize += (int)(fontScale*Mathf.Sign(n));
		fraseLabel.fontSize = Mathf.Clamp(fraseLabel.fontSize,55,100);
		fechaLabel.fontSize = Mathf.Clamp(fraseLabel.fontSize,55,100);
		if(b)
			PlayerPrefs.SetInt("FontSize",fraseLabel.fontSize);
	}

	public void FontSizeUp(){
		/*if(fraseLabel.GetComponent<UIWidget>().localSize.y <= 1150)
			FontSize(1, true);*/
		FontSizeApp(1);
	}

	public void FontSizeDown(){
		/*if(fraseLabel.GetComponent<UIWidget>().localSize.y >= 500)
			FontSize(-1, true);*/
		FontSizeApp(-1);
	}

	void FontSizeDownTemp(){
		if(fraseLabel.GetComponent<UIWidget>().localSize.y >= 500)
			FontSize(-1, false);
	}

	void FontSizeApp(int n){
		fraseLabel.fontSize += (int)(fontScale*Mathf.Sign(n));
		fraseLabel.fontSize = Mathf.Clamp(fraseLabel.fontSize,55,130);
		fechaLabel.fontSize += fraseLabel.fontSize;
		PlayerPrefs.SetInt("FontSize",fraseLabel.fontSize);
	}

	void FontSizeSSDown(){
		fraseLabelSS.fontSize -= (int)(1*fontScale);
		Debug.Log(fraseLabelSS.fontSize);
	}

	public void ResetNotifications(){
		Debug.Log("reset");
		PlayerPrefs.SetInt("Launched",0);
	}

	public void panelAlphaVisible(){
		StartCoroutine(share());
	}

	public void shareButton(){
		panelSS.alpha = 1;

		fraseLabelSS.text = fraseLabel.text;
		fraseLabelSS.fontSize = fraseLabel.fontSize;
		fechaLabelSS.text = fechaLabel.text;
		fechaLabelSS.fontSize = fechaLabel.fontSize;
		
		StartCoroutine(shareOnSocialMedia());
	}

	IEnumerator share(){
		panelSS.alpha = 1;
		fraseLabelSS.text = fraseLabel.text;
		//fraseLabelSS.fontSize = fraseLabel.fontSize;
		fechaLabelSS.text = fechaLabel.text;
		//fechaLabelSS.fontSize = fechaLabel.fontSize;
		Debug.Log(panelSS.alpha);
		yield return new WaitForSeconds(0.00001f);
		StartCoroutine(shareOnSocialMedia());
	}

	void FinishedSharing(eShareResult _result){
		//Debug.Log("done "+panelSS.alpha);
		panelSS.alpha = 0;
	}

	Texture2D Screenshot(){
		renderTexture = new RenderTexture(Screen.height, Screen.height, 24);
		SSCam.targetTexture = renderTexture;
		SSCam.Render();
		Texture2D tex = toTexture2D(renderTexture);
		
		int aux = (int)(tex.width);
		Color [] c = tex.GetPixels(0,0,aux,tex.height);
		Texture2D texFinal = new Texture2D(aux,tex.height);
		texFinal.SetPixels(c);
		texFinal.Apply();

		#if UNITY_EDITOR
		byte [] img = texFinal.EncodeToPNG();
		File.WriteAllBytes("test.png",img);
		#endif
		
		return texFinal;
	}

	Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.ARGB32, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

	IEnumerator shareOnSocialMedia(){
		panelSS.alpha = 1;
		yield return new WaitForSeconds(0f);
		SocialShareSheet _sharesheet = new SocialShareSheet();
		_sharesheet.Text = "\""+fraseLabel.text+"\"";
		if(fraseLabel.text.Length >= 280){
			_sharesheet.Text = _sharesheet.Text.Substring(0,275);
			_sharesheet.Text += "...\"";
		}
		_sharesheet.AttachImage(Screenshot());
		NPBinding.UI.SetPopoverPointAtLastTouchPosition();
		NPBinding.Sharing.ShowView(_sharesheet, FinishedSharing);
	}

	string carga = "";
	bool escribiendoCarga = false;
	public IEnumerator cargando(){
		escribiendoCarga = true;
		carga += ".";
		yield return new WaitForSeconds(0.2f);
		loading.text = I2.Loc.ScriptLocalization.Get("Loading")+carga;
		if(carga.Length >= 3){
			carga = "";
		}
		escribiendoCarga = false;
	}
}
