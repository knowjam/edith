using UnityEngine;
using System.Collections;

public class Tramp : MonoBehaviour {

	public Vector2 power = new Vector2(0.0f, 10.0f);
    public float cooltime = 1;
    public float currentCooltime = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        currentCooltime = Mathf.Clamp(currentCooltime - Time.deltaTime, 0, Mathf.Infinity);
	}

	void OnTriggerStay2D(Collider2D other) {

		if (other.tag == "Player" && currentCooltime <= 0) {

			other.gameObject.rigidbody2D.velocity += power;
            currentCooltime = cooltime;

		}

	}
}
