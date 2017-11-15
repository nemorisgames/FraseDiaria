using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableButton : MonoBehaviour {
	UIButton button;

	// Use this for initialization
	void Awake(){
		button = GetComponent<UIButton>();
	}

	public void toggleButton(){
		button.enabled = !button.enabled;
	}
}
