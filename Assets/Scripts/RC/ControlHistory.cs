﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Globalization;

public class ControlHistory : MonoBehaviour {
	public bool testing = false;
	public UIToggle[] meses;
	public GameObject fraseBotonPrefab;
	public Transform rootFrases;
	UIPanel panelFrases;
	System.DateTime startDay;
	int months = 0;
	ArrayList frasesCreadas = new ArrayList();
	public ControlPanelOpciones opciones;
	

	public int totalQuotes = 396;
	// Use this for initialization

	void Start () {
		if (PlayerPrefs.HasKey ("StartDay")) {
			startDay = System.DateTime.ParseExact ("12/1/2017 0:00:01 AM","M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture).Date;
		} else {
			//startDay = System.DateTime.Parse ("1/12/2016 0:00:01 AM").Date;
			startDay = System.DateTime.ParseExact ("12/1/2017 0:00:01 AM","M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture).Date;
			PlayerPrefs.SetString ("StartDay", startDay.ToString());
		}
		print ("Start day: " + startDay);
		panelFrases = rootFrases.GetComponent<UIPanel> ();

		System.DateTime date = System.DateTime.Now;
		if(testing) date = System.DateTime.ParseExact("12/31/2018 0:00:01 AM","M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture).Date;

		for (int i = 0; i <= 12; i++) {
			System.DateTime min = startDay.AddMonths (i);
			//Debug.Log(min.Month +"="+date.Month+","+ min.Year+"="+date.Year);
			if (min.Month == date.Month && min.Year == date.Year) {
				months = i;
			}
		}

		Debug.Log(months);

		//para el caso en que ha pasado más de un año
		if (date.Year > (startDay.AddMonths(13)).AddDays(1).Year) {
			months = 12;
		}
		
		//primero se desactivan y luego se activan
		for (int i = 0; i < 13; i++){
			meses [i].GetComponent<UIButton>().isEnabled = false;
			//meses [i].transform.Find("Label").GetComponent<UILabel>().color = opciones.GetColor(0);
		}
		for (int i = 0; i <= months; i++) {
			//int mesActual = startDay.Month + i;
			//Debug.Log("i: "+i+", mesActual: "+mesActual);
			meses [(i) % 13].GetComponent<UIButton>().isEnabled = true;
			//meses [(i) % 13].transform.Find("Label").GetComponent<UILabel>().color = opciones.GetColor(1);
		}

		meses [months].Set (true, false);
		mostrarFrases (months);
	}

	

	public void mostrarFrasesDiciembre0(){ mostrarFrases (0); }
	public void mostrarFrasesEnero(){ mostrarFrases (1); }
	public void mostrarFrasesFebrero(){ mostrarFrases (2); }
	public void mostrarFrasesMarzo(){ mostrarFrases (3); }
	public void mostrarFrasesAbril(){ mostrarFrases (4); }
	public void mostrarFrasesMayo(){ mostrarFrases (5); }
	public void mostrarFrasesJunio(){ mostrarFrases (6); }
	public void mostrarFrasesJulio(){ mostrarFrases (7); }
	public void mostrarFrasesAgosto(){ mostrarFrases (8); }
	public void mostrarFrasesSeptiembre(){ mostrarFrases (9); }
	public void mostrarFrasesOctubre(){ mostrarFrases (10); }
	public void mostrarFrasesNoviembre(){ mostrarFrases (11); }
	public void mostrarFrasesDiciembre(){ mostrarFrases (12); }

	void mostrarFrases(int indiceMesSeleccionado){
		for (int i = 0; i < frasesCreadas.Count; i++) {
			Destroy((GameObject)frasesCreadas[i]);
		}
		int indice = 0;
		frasesCreadas.Clear ();
		rootFrases.localPosition = new Vector3 (rootFrases.localPosition.x, 148f, rootFrases.localPosition.z);
		rootFrases.GetComponent<UIPanel> ().clipOffset = new Vector2(0f, 0f);
		System.DateTime date = System.DateTime.Now.Date;
		if(testing) date = System.DateTime.ParseExact("12/31/2018 0:00:01 AM","M/d/yyyy h:mm:ss tt",CultureInfo.InvariantCulture).Date;
		for (int i = 0; i <= Mathf.Clamp((date - startDay.Date).Days, 0, totalQuotes); i++) {
			if ((startDay.AddDays (i).Month == indiceMesSeleccionado && i >= 31) || (startDay.AddDays (i).Month == 12 && indiceMesSeleccionado == 0)) {
				//Debug.Log(i);
				GameObject g = Instantiate (fraseBotonPrefab);
				g.transform.parent = rootFrases;
				g.transform.localScale = Vector3.one;
				g.GetComponent<UISprite> ().width = (int)rootFrases.GetComponent<UIPanel> ().width;
				g.transform.localPosition = new Vector3 ((int)rootFrases.GetComponent<UIPanel> ().finalClipRegion.x + 20, 551f - indice * 190f, 0f);
				Quote q = g.GetComponent<Quote> ();
				//g.transform.Find("Quote").GetComponent<UILabel>().color = opciones.GetColor();
				//g.transform.Find("Date").GetComponent<UILabel>().color = opciones.GetColor();
				q.inicializar (i);
				frasesCreadas.Add (g);
				indice++;
			}
		}
		//meses[indiceMesSeleccionado].transform.Find("Label").GetComponent<UILabel>().color = opciones.GetColor(1);
	}

	public void cargarFrase(int indice){
		opciones.fade(false,opciones.sceneFade);
		PlayerPrefs.SetInt ("QuoteCheck", indice);
		//SceneManager.LoadScene ("Quote_NJ");
		StartCoroutine(cargarEscena());
		
	}

	public static IEnumerator cargarEscena(){
		yield return new WaitForSeconds(0.25f);
		AsyncOperation async = SceneManager.LoadSceneAsync("Quote_NJ");
		yield return async;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
