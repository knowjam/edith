using UnityEngine;
using System.Collections;
using System;

public class Teleport : EdMonoBehaviour
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
        var player = GameObject.Find("Player");

        player.GetComponent<Player>().lastCheckpointAreaPosition = Vector3.zero;

        if (other.transform.tag == "Player")
        {
            if (Application.loadedLevelName == "Stage3")
            {
                LoadLevelWithSceneFade("MainMenu");
            }
            else if (Application.loadedLevelName == "Stage1" || Application.loadedLevelName == "Stage2")
            {
                if (player)
                {
                    player.GetComponent<Player>().transferedObject = true;
                    UnityEngine.Object.DontDestroyOnLoad(player);
                }

                LoadLevelWithSceneFade(nextStageName);
            }
            else
            {
                throw new Exception("Unknown stage");
            }
        }
    }
}
