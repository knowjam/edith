using UnityEngine;
using System.Collections;

public class RopeArea : MonoBehaviour
{

    public bool isRope = false;
    private Player playerObject;

    bool ropeKeyPressed
    {
        get
        {
            return Input.GetKeyDown(KeyCode.R) || RopeButton.ropeButtonHit;
        }
    }

    bool climbKeyPressed
    {
        get
        {
            return Input.GetKeyDown(KeyCode.UpArrow) || VirtualJoystickRegion.VJRnormals.y > 0.3f;
        }
    }

    // Use this for initialization
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!isRope && other.gameObject.tag == "Player" && ropeKeyPressed)
        {
            if (playerObject.haveRope)
            {
                isRope = true;
                playerObject.resetRope();
                // 로프를 생성하는 구문을 놔야 됩니다.

                var obj = Instantiate(playerObject.climbRopePrefab, transform.position, Quaternion.identity) as GameObject;
                obj.transform.localScale = new Vector2(obj.transform.localScale.x, ((BoxCollider2D)gameObject.collider2D).size.y);
            }
        }


        if (isRope && other.gameObject.tag == "Player" && climbKeyPressed)
        {
            other.gameObject.transform.position = new Vector3(transform.position.x, other.transform.position.y + 0.1f, 8.0f);
            other.gameObject.SendMessage("Climb");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("resetClimb");
        }
    }
}

