using UnityEngine;
using System.Collections;

public class RopeButton : MonoBehaviour
{
    public static bool ropeButtonHit;

    void Update()
    {
        GetComponent<GUITexture>().enabled = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().haveRope;

        if (GetComponent<GUITexture>().enabled)
        {
            ropeButtonHit = false;
            foreach (var t in Input.touches)
            {
                if (GetComponent<GUITexture>().HitTest(t.position))
                {
                    ropeButtonHit = true;
                    break;
                }
            }
        }
    }
}
