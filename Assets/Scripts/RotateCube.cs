using UnityEngine;
using System.Collections;

public class RotateCube : MonoBehaviour {

	float fDirection;
	float fLEFT = 1.0f;
	float fRIGHT = -1.0f;
	bool bRotate;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (bRotate) {			
			if (transform.localRotation.eulerAngles.z <= 5) {
				fDirection = fLEFT;
			}
			if (transform.localRotation.eulerAngles.z > 160) {
				fDirection = fRIGHT;
			}		
			transform.Rotate(0.0f, 0.0f, fDirection);
		}			
	}

	void OnCollisionEnter2D(Collision2D col){
		bRotate = !bRotate;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Rotate") {
			bRotate = true;
		}
	}
}
