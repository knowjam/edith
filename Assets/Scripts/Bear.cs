using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Bear : MonoBehaviour
{
    public GameObject particle;

    void Start()
    {
    }

    void Update()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject == null)
        {
            Debug.LogError("Player 객체를 찾을 수 없습니다");
            return;
        }

        var player = playerObject.GetComponent<Player>();

        if (Input.GetKey(KeyCode.A)
            && player.playerMode == Player.PlayerMode.Snake)
        {
            var frontObject = player.GetItemAttackRayCheckResult();

            if (frontObject == gameObject)
            {
                ++player.killCount;

                GameObject.Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        bool dir = true;
        if (particle)
        {
            GameObject.Instantiate(particle, new Vector3(other.transform.position.x, other.transform.position.y + 0.5f, other.transform.position.z - 0.5f), Quaternion.identity);
        }

        if (transform.position.x - other.gameObject.transform.position.x < 0.0f)
        {
            dir = false;
        }

        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("Back", new KnockbackInfo { pusher = this, left = dir });
        }
    }

    void Back(KnockbackInfo info)
    {
        if (info.left)
        {
            transform.rigidbody2D.velocity = new Vector2(-50, transform.rigidbody2D.velocity.y);
        }
        else
        {
            transform.rigidbody2D.velocity = new Vector2(+50, transform.rigidbody2D.velocity.y);
        }
    }
}
