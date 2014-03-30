using UnityEngine;
using System.Collections;

public class RotationReverseSwitch : MonoBehaviour
{
    public GameObject ObjectToBeReverseRotated;
    private bool pushed;
    public Texture2D notPushedTex;
    public Texture2D pushedTex;

    void OnTriggerEnter2D(Collider2D other)
    {
        renderer.material.mainTexture = pushedTex;

        if (!pushed)
        {
            pushed = true;

            ObjectToBeReverseRotated.GetComponent<CogwheelRotate>().speed *= -1;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        renderer.material.mainTexture = notPushedTex;
    }
}
