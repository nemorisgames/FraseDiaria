using UnityEngine;
using System.Collections;

public class Quote : MonoBehaviour {
	public int indiceFrase = 0;
	public UILabel fraseLabel;
	public UILabel fechaLabel;
	// Use this for initialization
	void Start () {
		
	}

	public void inicializar(int indice){
		indiceFrase = indice;
		fraseLabel.GetComponent<I2.Loc.Localize>().SetTerm ("Quote" + (indice + 1));
		string completo = I2.Loc.ScriptLocalization.Get("Quote" + (indice + 1));
		string[] partes = completo.Split ('|');

		if (partes.Length > 1) {
			fraseLabel.text = partes [0];

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

	public void verFrase(){
		Camera.main.GetComponent<ControlHistory> ().cargarFrase (indiceFrase);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
