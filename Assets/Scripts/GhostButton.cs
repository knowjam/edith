using UnityEngine;
using System.Collections;

public class GhostButton : MonoBehaviour
{
    public static bool ghostButtonHit;

    void Update()
    {
        GetComponent<GUITexture>().enabled = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().haveBlanket;

        if (GetComponent<GUITexture>().enabled)
        {
            ghostButtonHit = false;
            foreach (var t in Input.touches)
            {
                if (GetComponent<GUITexture>().HitTest(t.position))
                {
                    ghostButtonHit = true;
                    break;
                }
            }
        }
    }
}
