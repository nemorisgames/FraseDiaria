using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlHistory : MonoBehaviour {
	public UIToggle[] meses;
	public GameObject fraseBotonPrefab;
	public Transform rootFrases;
	UIPanel panelFrases;
	System.DateTime startDay;
	int months = 0;
	ArrayList frasesCreadas = new ArrayList();

	int totalQuotes = 365;
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey ("StartDay")) {
			startDay = System.DateTime.Parse ("1/1/2016 10:32:21 PM").Date;//PlayerPrefs.GetString ("StartDay")); //
		} else {
			startDay = System.DateTime.Parse ("1/1/2016 10:32:21 PM").Date;
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
			meses [(mesActual - 1) % 12].GetComponent<UIButton>().isEnabled = true;
		}

		meses [System.DateTime.Now.Month - 1].Set (true, false);
		mostrarFrases (System.DateTime.Now.Month - 1);
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
		for (int i = 0; i < frasesCreadas.Count; i++) {
			Destroy((GameObject)frasesCreadas[i]);
		}
		int indice = 0;
		frasesCreadas.Clear ();
		rootFrases.localPosition = new Vector3 (rootFrases.localPosition.x, -79f, rootFrases.localPosition.z);
		rootFrases.GetComponent<UIPanel> ().clipOffset = new Vector2(0f, 0f);
		for (int i = 0; i <= Mathf.Clamp((System.DateTime.Now.Date - startDay.Date).Days, 0, totalQuotes); i++) {
			if (startDay.AddDays (i).Month == indiceMesSeleccionado + 1) {
				GameObject g = Instantiate (fraseBotonPrefab);
				g.transform.parent = rootFrases;
				g.transform.localScale = Vector3.one;
				g.GetComponent<UISprite> ().width = (int)rootFrases.GetComponent<UIPanel> ().width;
				g.transform.localPosition = new Vector3 ((int)rootFrases.GetComponent<UIPanel> ().finalClipRegion.x, 785f - indice * 190f, 0f);
				Quote q = g.GetComponent<Quote> ();
				q.inicializar (i);
				frasesCreadas.Add (g);
				indice++;
			}
		}
	}

	public void cargarFrase(int indice){
		PlayerPrefs.SetInt ("QuoteCheck", indice);
		SceneManager.LoadScene ("Quote_RC");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
