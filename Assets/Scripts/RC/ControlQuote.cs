using UnityEngine;
using System.Collections;

public class ControlQuote : MonoBehaviour {

	public UILabel fraseLabel;
	public UILabel autorLabel;
	public UILabel fechaLabel;
	public UITexture imagenFondo;
	public Texture[] imagenes;
	System.DateTime startDay;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("StartDay")) {
			startDay = System.DateTime.Parse ("1/1/2016 10:32:21 PM").Date;//PlayerPrefs.GetString ("StartDay")); //
		} else {
			startDay = System.DateTime.Parse ("1/1/2016 10:32:21 PM").Date;
			PlayerPrefs.SetString ("StartDay", startDay.ToString());
		}

		cargarFrase ();
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
	
	}
}
