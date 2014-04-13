using UnityEngine;
using System.Collections;
using System;

public class Teleport : MonoBehaviour
{
    public string nextStageName;

    void Start()
    {
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (Application.loadedLevelName == "Stage3")
            {
                Application.LoadLevel("MainMenu");
            }
            else if (Application.loadedLevelName == "Stage1" || Application.loadedLevelName == "Stage2")
            {
                var player = GameObject.Find("Player");
                player.GetComponent<Player>().transferedObject = true;
                UnityEngine.Object.DontDestroyOnLoad(player);

                Application.LoadLevel(nextStageName);
            }
            else
            {
                throw new Exception("Unknown stage");
            }
        }
    }
}
