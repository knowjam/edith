using UnityEngine;
using System.Collections;

public class Bear : MonoBehaviour
{
    public GameObject particle;

    void Start()
    {
    }

    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Player>().playerMode == Player.PlayerMode.Snake
            && Input.GetKey(KeyCode.A)
            && (GameObject.Find("Player").transform.position - transform.position).magnitude < 3.0f) // 곰과 같이 큰 오브젝트와 개와 같이 작은 오브젝트가 공존하고 있으므로 이런 식으로 고정값으로 체크하는 것은 말이 안된다. 수정하자~
        {
            ++GameObject.Find("Player").GetComponent<Player>().killCount;

            GameObject.Destroy(gameObject);
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
