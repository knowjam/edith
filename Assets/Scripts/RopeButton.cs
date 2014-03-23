using UnityEngine;
using System.Collections;

public class RopeButton : MonoBehaviour
{
    public static bool ropeButtonHit;

    void Update()
    {
        guiTexture.enabled = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().haveRope;

        if (guiTexture.enabled)
        {
            ropeButtonHit = false;
            foreach (var t in Input.touches)
            {
                if (guiTexture.HitTest(t.position))
                {
                    ropeButtonHit = true;
                    break;
                }
            }
        }
    }
}
