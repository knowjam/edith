using UnityEngine;
using System.Collections;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollCoefficient = 1;
    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (!player)
        {
            return;
        }

        var x = player.transform.position.x / transform.localScale.x * scrollCoefficient;
        x -= (int)x;

        if (x > 1)
        {
            x -= 1;
        }
        else if (x < -1)
        {
            x += 1;
        }

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x, 0);
    }
}
