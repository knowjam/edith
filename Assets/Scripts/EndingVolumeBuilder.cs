using UnityEngine;
using System.Collections;

public class EndingVolumeBuilder : MonoBehaviour
{

    public GameObject happyEndingVolumePrefab;
    public GameObject badEndingVolumePrefab;

    bool once;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (once == false && other.tag == "Player")
        {
            once = true;

            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().playerMode != Player.PlayerMode.Snake)
            {
                Instantiate(happyEndingVolumePrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(badEndingVolumePrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
