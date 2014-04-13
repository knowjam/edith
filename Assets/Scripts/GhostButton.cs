using UnityEngine;
using System.Collections;

public class GhostButton : MonoBehaviour
{
    public static bool ghostButtonHit;

    void Update()
    {
        guiTexture.enabled = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().haveBlanket;

        if (guiTexture.enabled)
        {
            ghostButtonHit = false;
            foreach (var t in Input.touches)
            {
                if (guiTexture.HitTest(t.position))
                {
                    ghostButtonHit = true;
                    break;
                }
            }
        }
    }
}
