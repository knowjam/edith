using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class OneWayPlatform : MonoBehaviour
{
    private bool oneway;

    void Start()
    {
    }

    void Update()
    {
        transform.parent.GetComponent<BoxCollider2D>().enabled = !oneway;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            oneway = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            oneway = false;
        }
    }
}
