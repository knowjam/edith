using UnityEngine;
using System.Collections;

public class CollideParticle : MonoBehaviour {

	private float duration;
	// Use this for initialization
	void Start () {
		duration = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		duration -= Time.deltaTime;
		if ( duration <= 0.0f)
			GameObject.Destroy(gameObject);
	}
}
