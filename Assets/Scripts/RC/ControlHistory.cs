using UnityEngine;
using System.Collections;

public class ControlHistory : MonoBehaviour {
	public UIToggle[] meses;
	public GameObject fraseBotonPrefab;
	public Transform rootFrases;
	UIPanel panelFrases;
	System.DateTime startDay;
	int months = 0;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("StartDay")) {
			startDay = System.DateTime.Parse ("11/4/2015 10:32:21 PM");//PlayerPrefs.GetString ("StartDay")); //
		} else {
			startDay = System.DateTime.Now;
			PlayerPrefs.SetString ("StartDay", startDay.ToString());
		}
		print ("Start day: " + startDay);
		panelFrases = rootFrases.GetComponent<UIPanel> ();
		for (int i = 1; i <= 12; i++) {
			System.DateTime min = startDay.AddMonths (i);
			if (min.Month == System.DateTime.Now.Month && min.Year == System.DateTime.Now.Year) {
				months = i;
			}
		}
		//para el caso en que ha pasado más de un año
		if ((months == 0 && startDay.AddDays(31) < System.DateTime.Now) || months > 12) {
			months = 12;
		}
		//primero se desactivan y luego se activan
		for (int i = 0; i < 12; i++) meses [i].GetComponent<UIButton>().isEnabled = false;
		for (int i = 0; i <= months; i++) {
			int mesActual = startDay.Month + i;
			meses [(mesActual - 1)%12].GetComponent<UIButton>().isEnabled = true;
		}
	}

	public void mostrarFrasesEnero(){ mostrarFrases (0); }
	public void mostrarFrasesFebrero(){ mostrarFrases (1); }
	public void mostrarFrasesMarzo(){ mostrarFrases (2); }
	public void mostrarFrasesAbril(){ mostrarFrases (3); }
	public void mostrarFrasesMayo(){ mostrarFrases (4); }
	public void mostrarFrasesJunio(){ mostrarFrases (5); }
	public void mostrarFrasesJulio(){ mostrarFrases (6); }
	public void mostrarFrasesAgosto(){ mostrarFrases (7); }
	public void mostrarFrasesSeptiembre(){ mostrarFrases (8); }
	public void mostrarFrasesOctubre(){ mostrarFrases (9); }
	public void mostrarFrasesNoviembre(){ mostrarFrases (10); }
	public void mostrarFrasesDiciembre(){ mostrarFrases (11); }

	void mostrarFrases(int indiceMesSeleccionado){
		
		int indiceInicial = 0;
		int indiceFinal = (System.DateTime.Now - startDay).Days;
		//arreglar año
		if (System.DateTime.Parse ((indiceMesSeleccionado + 1) + "/01/2016") > startDay) {
			if (System.DateTime.Parse ((indiceMesSeleccionado + 1) + "/" + System.DateTime.DaysInMonth (2016, indiceMesSeleccionado + 1) + "/2016") < System.DateTime.Now) {
				indiceInicial = (System.DateTime.Parse ((indiceMesSeleccionado + 1) + "/01/2016") - startDay).Days + 1;
				indiceFinal = indiceInicial + System.DateTime.DaysInMonth (2016, indiceMesSeleccionado + 1) - 1;
			} else {
				indiceInicial = (System.DateTime.Parse ((indiceMesSeleccionado + 1) + "/01/2016") - startDay).Days + 1;
				indiceFinal = indiceInicial + System.DateTime.Now.Day - 1;
			}
		} else {
			//caso en que la fecha inicial está dentro del mes seleccionado y la fecha final despues
			if (System.DateTime.Parse ((indiceMesSeleccionado + 1) + "/" + System.DateTime.DaysInMonth (2016, indiceMesSeleccionado + 1) + "/2016") < System.DateTime.Now) {
				indiceInicial = 0;
				indiceFinal = (System.DateTime.DaysInMonth (2016, indiceMesSeleccionado + 1) - startDay.Day);
			} 
			//Caso con fecha inicial y final en el mismo mes y con frases en el mismo mes
			else {
				indiceInicial = 0;
				indiceFinal = (System.DateTime.Now.Day - startDay.Day);
			}
		}
		print (indiceInicial + " - " + indiceFinal);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
