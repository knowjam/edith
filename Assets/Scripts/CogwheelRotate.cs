using UnityEngine;
using System.Collections;

public class CogwheelRotate : MonoBehaviour
{
    public float speed = 10;
    public bool speedOnTouchOther;
    public bool speedReverse;

    void Start()
    {
    }

    void Update()
    {
        if (speedOnTouchOther)
        {

        }

        transform.Rotate(0, 0, speed*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (speedOnTouchOther)
        {
            var cogwheelComp = other.GetComponent<CogwheelRotate>();
            if (cogwheelComp)
            {
                speed = cogwheelComp.speed;
            }
        }
    }

    void OnMoveOnceCompleted()
    {
        speed = -50;

        if (speedReverse)
        {
            speed *= -1;
        }
    }
}
