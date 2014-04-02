using UnityEngine;
using System.Collections;

public class CheckpointArea : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            player.SetCheckpoint(this);
        }
    }
}
