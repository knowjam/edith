using UnityEngine;
using System.Collections;

public class MoveBlockX : MonoBehaviour {

	int iLEFT = -1;
	int iRIGHT = 1;
	int iSpeed = 2;
	int iDirection;
	float fStartX;

	// Use this for initialization
	void Start () {
		fStartX = transform.position.x;	
	}

	// Update is called once per frame
	void Update () {

		if (transform.position.x <= fStartX) {
			iDirection = iRIGHT;
		}
		if (transform.position.x >= fStartX + 14.0f) {
			iDirection = iLEFT;
		}
		transform.Translate (iDirection * iSpeed * Time.deltaTime, 0, 0);
	}
}
