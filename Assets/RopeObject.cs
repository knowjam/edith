using UnityEngine;
using System.Collections;

public class RopeObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject playerObject =  GameObject.Find("Player");

		if ( playerObject == null )
			Debug.Log ("Player 객체를 찾을 수 없습니다");

		if (Input.GetKeyDown(KeyCode.A) && !playerObject.GetComponent<Player>().isRope() && ( playerObject.transform.position - transform.position).magnitude < 3.0f)
		{
			playerObject.SendMessage("getSnake"); 
			GameObject.Destroy(gameObject);
		}
	
	}

}

