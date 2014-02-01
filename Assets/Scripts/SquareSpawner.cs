using UnityEngine;
using System.Collections;

public class SquareSpawner : MonoBehaviour {

	public GameObject spawnPrefab;
	public float spawnInterval = 2.0f;

	float spawnGauge;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		spawnGauge += Time.deltaTime;

		if (spawnGauge > spawnInterval)
		{
			GameObject.Instantiate(spawnPrefab, transform.position, spawnPrefab.transform.rotation);

			spawnGauge = 0;
		}
	}
}
