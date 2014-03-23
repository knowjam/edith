using UnityEngine;
using System.Collections;

public class GhostObject : MonoBehaviour
{
    void Update()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject == null)
        {
            Debug.Log("Player 객체를 찾을 수 없습니다");
        }

        // 'A'키를 눌렀다는 것은 유령을 무기로서 취득하려고 했다는 뜻이다.
        if (Input.GetKeyDown(KeyCode.A)
            && playerObject.GetComponent<Player>().playerMode == Player.PlayerMode.NotDetermined
            && (playerObject.transform.position - transform.position).magnitude < 1.0f)
        {
            playerObject.SendMessage("getGhost");
            GameObject.Destroy(gameObject);
        }
    }
}
