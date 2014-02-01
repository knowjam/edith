using UnityEngine;
using System.Collections;

public class MovePlatform : MonoBehaviour {

	public bool leftRight = true; // true면 좌우이동, false면 상하이동
	public float moveAmount;
	public float moveVelocity;
	private float startPosition;
	private bool curMoveLeft;

	// Use this for initialization
	void Start () {
		if (leftRight)
		{
			startPosition = transform.position.x;
			transform.rigidbody2D.velocity = new Vector2( moveVelocity,  0.0f);
		}
		else
		{
			startPosition = transform.position.y;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (curMoveLeft)
		{
			if (leftRight)
			{
				//transform.position.x -= Time.deltaTime * moveVelocity;
				//transform.position = new Vector3(transform.position.x - Time.deltaTime * moveVelocity, transform.position.y, transform.position.z);

				if (startPosition > transform.position.x)
				{
					curMoveLeft = false;
					transform.rigidbody2D.velocity = new Vector2( moveVelocity,  0.0f);
				}
			}
			else
			{
				//transform.position.y -= Time.deltaTime * moveVelocity;
				transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * moveVelocity, transform.position.z);

				if (startPosition > transform.position.y)
				{
					curMoveLeft = false;
				}
			}


		}
		else
		{
			if (leftRight)
			{
				//transform.position.x += Time.deltaTime * moveVelocity;
				//transform.position = new Vector3(transform.position.x + Time.deltaTime * moveVelocity, transform.position.y, transform.position.z);


				if (startPosition + moveAmount < transform.position.x)
				{
					curMoveLeft = true;
					transform.rigidbody2D.velocity = new Vector2( -moveVelocity,  0.0f);
				}
			}
			else
			{
				//transform.position.y += Time.deltaTime * moveVelocity;
				transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * moveVelocity, transform.position.z);

				if (startPosition + moveAmount < transform.position.y)
				{
					curMoveLeft = true;
				}
			}
		}
	}
}
