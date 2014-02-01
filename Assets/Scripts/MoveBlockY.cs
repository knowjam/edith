using UnityEngine;
using System.Collections;

public class MoveBlockY : MonoBehaviour {
	
	int iDOWN = -1;
	int iUP = 1;
	int iSpeed = 2;
	int iDirection;
	float fStartY;

	// Use this for initialization
	void Start () {
		fStartY = transform.position.y;		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= fStartY) {
			iDirection = iUP;
		}
		if (transform.position.y >= fStartY + 9.0f) {
			iDirection = iDOWN;
		}
		transform.Translate (0, iDirection * iSpeed * Time.deltaTime, 0);	
	}
}
