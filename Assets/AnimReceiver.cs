using UnityEngine;
using System.Collections;

public class AnimReceiver : MonoBehaviour
{
    void PrintFloat(float v)
    {
        var p = transform.parent.GetComponent<Player>();

        p.SwitchToNormalMesh();
    }
}
