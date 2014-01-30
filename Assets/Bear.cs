using UnityEngine;
using System.Collections;

public class Bear : MonoBehaviour {

	public GameObject particle;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.Find("Player").GetComponent<Player>().isSnake() && Input.GetKey (KeyCode.A) && ( GameObject.Find("Player").transform.position - transform.position).magnitude < 3.0f) {

			GameObject.Destroy(gameObject);
		}
	
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		bool dir = true;
		if (particle)
		{
			GameObject.Instantiate( particle , new Vector3( other.transform.position.x, other.transform.position.y + 0.5f, other.transform.position.z -  0.5f) , Quaternion.identity );
		}

		if ( transform.position.x - other.gameObject.transform.position.x < 0.0f ) 
			dir = false;
		if ( other.gameObject.tag == "Player" )
			other.gameObject.SendMessage("Back", dir);

	}
}
