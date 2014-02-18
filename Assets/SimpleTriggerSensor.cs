using UnityEngine;
using System.Collections;

public class SimpleTriggerSensor : MonoBehaviour
{
    [HideInInspector]
    public bool triggered;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            triggered = true;
        }
    }

    void OnTriggerLeave2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            triggered = false;
        }
    }
}
