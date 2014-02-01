using UnityEngine;
using System.Collections;

public class ActivationLine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        var spawner = other.gameObject.GetComponent<Spawner>();
        if (spawner && spawner.EnemyPrefab)
        {
            GameObject.Instantiate(spawner.EnemyPrefab,
                other.gameObject.transform.position, Quaternion.EulerRotation(0, -90, 0));
        }
    }
}

