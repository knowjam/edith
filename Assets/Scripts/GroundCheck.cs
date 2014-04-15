using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour
{
    public bool passthroughByJump;

    void Start()
    {
    }

    void Update()
    {
        var p = GameObject.Find("PlayerFoot");
        if (!p)
        {
            return;
        }
   
        var playerY = p.transform.position.y;
        var groundY = transform.position.y + transform.localScale.y/2;
        var groundSkin = -0.05;
        if (!passthroughByJump || groundY + groundSkin < playerY)
        {
            gameObject.layer = LayerMask.NameToLayer("Ground");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("GroundIgnored");
        }
    }
}
