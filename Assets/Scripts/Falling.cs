using UnityEngine;
using System.Collections;

public class Falling : MonoBehaviour {

	public float fallSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate(0, -fallSpeed * Time.deltaTime, 0, Space.World);

		if (transform.position.y < -2) {
			GameObject.Destroy(gameObject);
		}
	
	}
}
