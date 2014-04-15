using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour
{
    public bool passthroughByJump;

    void Start()
    {
        if (gameObject.layer == LayerMask.NameToLayer("GothroughGround"))
        {
            Debug.LogError(string.Format("{0}: 유령으로 통과할 수 있는 바닥에는 이 스크립트가 사용되지 않아야 합니다.", gameObject.name));
        }
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
