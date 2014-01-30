using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	public bool leftToRightScroll;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (leftToRightScroll)
		{
			float x = GameObject.Find("Player").transform.position.x;

			transform.position = new Vector3(
				x,
				transform.position.y,
				transform.position.z);
		}
		else
		{
			float y = GameObject.Find("Player").transform.position.y;

			transform.position = new Vector3(
				transform.position.x,
				y,
				transform.position.z);
		}
	}
}