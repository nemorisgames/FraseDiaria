using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OptionsPopupList : MonoBehaviour {

	private string _savedValue = "null";

	void Start(){
		var popup = GetComponent<UIPopupList> ();
		popup.value = "|||";
		EventDelegate.Add (popup.onChange, delegate() {
			_savedValue = popup.value;
			if (_savedValue != "|||")
				ChangeScene ();
		});
	}



	void ChangeScene(){
		SceneManager.LoadScene (_savedValue);

		/*
		if (savedValue == "History") {
			SceneManager.LoadScene ("History");
		}
		if (savedValue == "Configuration") {
			SceneManager.LoadScene ("Configuration");
		}
		if (savedValue == "About") {
			SceneManager.LoadScene ("About");
		}
		*/
	}
		
}
