using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Take off from the ground");

            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "BottomSensor")
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

}
