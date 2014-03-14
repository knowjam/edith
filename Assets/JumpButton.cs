using UnityEngine;
using System.Collections;

public class JumpButton : MonoBehaviour
{
    public static bool jumpButtonHit;

    void Update()
    {
        jumpButtonHit = false;
        foreach (var t in Input.touches)
        {
            if (guiTexture.HitTest(t.position))
            {
                jumpButtonHit = true;
                break;
            }
        }
    }
}
