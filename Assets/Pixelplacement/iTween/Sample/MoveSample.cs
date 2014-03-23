using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed;

    void Start()
    {
        //iTween.MoveBy(gameObject, iTween.Hash("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
        iTween.MoveTo(gameObject, iTween.Hash("path", waypoints, "speed", speed, "easetype", "linear", "looptype", "loop", "orientToPath", true, "lookTime", 0.2));

    }

    void OnDrawGizmos()
    {
        iTween.DrawPath(waypoints);
    }
}

