using UnityEngine;
using System.Collections;

public class CogwheelRotate : MonoBehaviour
{
    public float speed = 10;

    void Start()
    {
    }

    void Update()
    {
        transform.Rotate(0, 0, speed*Time.deltaTime);
    }
}
