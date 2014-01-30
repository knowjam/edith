using UnityEngine;
using System.Collections;

public class Tramp : MonoBehaviour {

	public Vector2 power = new Vector2(0.0f, 10.0f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") {

			other.gameObject.rigidbody2D.velocity += power;

		}

	}
}
