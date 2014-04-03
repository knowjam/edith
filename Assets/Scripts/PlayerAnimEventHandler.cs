using UnityEngine;
using System.Collections;

public class PlayerAnimEventHandler : MonoBehaviour
{
    public GameObject itemAttackRangeObject;

    void OnAnimIdle()
    {
        itemAttackRangeObject.GetComponent<TrailRenderer>().enabled = false;
    }

    void OnAnimEventAttackHitStart()
    {
        itemAttackRangeObject.GetComponent<TrailRenderer>().enabled = true;
    }

    void OnAnimEventAttackHitTime()
    {
        var player = GameObject.Find("Player").GetComponent<Player>();

        if (player.playerMode == Player.PlayerMode.NotDetermined)
        {
            var frontObject = player.GetItemAttackRayCheckResult();

            if (frontObject)
            {
                if (frontObject.CompareTag("RopeObject"))
                {
                    player.SendMessage("getSnake");
                    GameObject.Destroy(frontObject);
                }
                else if (frontObject.CompareTag("GhostObject"))
                {
                    player.SendMessage("getGhost");
                    GameObject.Destroy(frontObject);
                }
            }
        }
    }

}
