using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		
	if (other.transform.tag == "Player") {

			if (Application.loadedLevel == 2)
				Application.LoadLevel(0);
			else
				Application.LoadLevel(2);
		}
	}
}
