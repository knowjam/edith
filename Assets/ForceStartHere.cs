using UnityEngine;
using System.Collections;

public class ForceStartHere : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (Application.isEditor)
        {
            GameObject.Find("Player").transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
