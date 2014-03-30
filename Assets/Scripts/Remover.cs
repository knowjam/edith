﻿using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour
{
    public GameObject ObjectToBeRemoved;
    private bool pushed;
    public Texture2D notPushedTex;
    public Texture2D pushedTex;

    void OnTriggerEnter2D(Collider2D other)
    {
        renderer.material.mainTexture = pushedTex;

        if (!pushed)
        {
            pushed = true;

            GameObject.Destroy(ObjectToBeRemoved);
        }

        //GameObject.Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        renderer.material.mainTexture = notPushedTex;
    }
}
