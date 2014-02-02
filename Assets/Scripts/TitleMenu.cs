using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour {
	
	public GUISkin BtnSkin;

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI() {   
		if (GUI.Button(new Rect (Screen.width / 2 + 15, Screen.height / 2 + 40, 110, 30), " Start", BtnSkin.button)) {
			Application.LoadLevel(1);
		}

        //if (GUI.Button (new Rect (Screen.width / 2 + 15, Screen.height / 2 + 70, 110, 30), "End", BtnSkin.button)) {
        //}
	}
}
