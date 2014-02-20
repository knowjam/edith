using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        var p = GameObject.Find("PlayerFoot");
        var playerY = p.transform.position.y;
        var groundY = transform.position.y + transform.localScale.y/2;
        var groundSkin = -0.05;
        if (groundY + groundSkin < playerY)
        {
            gameObject.layer = LayerMask.NameToLayer("Ground");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("GroundIgnored");
        }
    }
}
