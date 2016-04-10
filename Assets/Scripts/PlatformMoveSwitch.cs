using UnityEngine;
using System.Collections;

public class PlatformMoveSwitch : MonoBehaviour
{
    public GameObject ObjectToBeMoved;
    private bool pushed;
    public Texture2D notPushedTex;
    public Texture2D pushedTex;
    public float targetVelocity;

    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Renderer>().material.mainTexture = pushedTex;

        if (!pushed)
        {
            pushed = true;

            ObjectToBeMoved.GetComponent<MovePlatform>().moveVelocity = targetVelocity;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GetComponent<Renderer>().material.mainTexture = notPushedTex;
    }
}
