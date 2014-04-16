using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class FontSizeAdjuster : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GetComponent<GUIText> ().fontSize = (int)(Screen.width / 1280.0f * 50);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
