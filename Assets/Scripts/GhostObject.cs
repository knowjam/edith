using UnityEngine;
using System.Collections;

public class GhostObject : MonoBehaviour
{
    void Update()
    {
        GameObject playerObject = GameObject.Find("Player");
        
        if (playerObject == null)
        {
            Debug.LogError("Player 객체를 찾을 수 없습니다");
            return;
        }

        var player = playerObject.GetComponent<Player>();

        // 'A'키를 눌렀다는 것은 유령을 무기로서 취득하려고 했다는 뜻이다.
        if (Input.GetKeyDown(KeyCode.A)
            && player.playerMode == Player.PlayerMode.NotDetermined)
        {
            var frontObject = player.GetItemAttackRayCheckResult();

            //Debug.Log(frontObject);
            
            if (frontObject == gameObject)
            {
                playerObject.SendMessage("getGhost");
                GameObject.Destroy(gameObject);
            }
        }
    }
}
