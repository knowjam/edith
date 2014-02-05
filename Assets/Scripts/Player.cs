using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float jumpVelocity = 20.0f;
    public float walkVelocity_init = 3.0f;
    public float jumpBoost_init = 1.5f;
    public float gravityScale_init = 10.0f;
    public int knockBackFrame = 50;
    private bool isGrounded;
    private bool isClimbing;
    private bool gravitiyScale;
    private int isKnockBack;
    public AudioClip jumpAudioClip;
    public AudioClip attackAudioClip;
    public AudioClip knockbackAudioClip;
    public GameObject climbRopePrefab;
    private float walkVelocity;
    private bool haveRope; // 로프를 가지고 있는 지 판단한다.
    private bool haveSnake;
    public bool happyEnding;

    // Use this for initialization
    void Start()
    {
        isClimbing = false;
        rigidbody2D.gravityScale = gravityScale_init;

        haveRope = false;
        haveSnake = false;

        isKnockBack = knockBackFrame + 1;
        walkVelocity = walkVelocity_init;
        //	accelation_y = 0.0f;
        //	last_velocity_y = transform.rigidbody2D.velocity.y;
    }


    // Update is called once per frame
    void Update()
    {
        //	accelation_y = (transform.rigidbody2D.velocity.y - last_velocity_y)/Time.deltaTime;
        //	last_velocity_y = transform.rigidbody2D.velocity.y;

        //	rigidbody2D.velocity = new Vector2( 0.0f , rigidbody2D.velocity.y );
        if (isKnockBack <= knockBackFrame)
        {
            isKnockBack++;
            return;
        }

        if (isClimbing == true)
            return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody2D.velocity = new Vector2(-walkVelocity, rigidbody2D.velocity.y);

            transform.rotation = Quaternion.Euler(0, 180, 0);

            GameObject.Find("PlayerMesh").GetComponent<Animator>().SetInteger("state", 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody2D.velocity = new Vector2(walkVelocity, rigidbody2D.velocity.y);

            transform.rotation = Quaternion.Euler(0, 0, 0);

            GameObject.Find("PlayerMesh").GetComponent<Animator>().SetInteger("state", 1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            GameObject.Find("PlayerMesh").GetComponent<Animator>().SetInteger("state", 3);

            audio.PlayOneShot(attackAudioClip);
        }
        else
        {
            GameObject.Find("PlayerMesh").GetComponent<Animator>().SetInteger("state", 0);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rigidbody2D.velocity = new Vector2(0.0f, rigidbody2D.velocity.y);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rigidbody2D.velocity = new Vector2(0.0f, rigidbody2D.velocity.y);
        }

    }



    void Climb()
    {
        if (isClimbing == false)
            setClimb();

        rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, walkVelocity);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -walkVelocity);
        }
    }


    void OnCollisionStay2D(Collision2D other)
    {

        if (other.transform.tag == "RopeObject" && (haveRope == false))
        {
            haveRope = true;
            GameObject.Destroy(other.gameObject);
        }

        //	if (other.transform.tag == "Ground") {

        //	var otherCollider = (BoxCollider2D)other.collider;
        //	if (transform.position.y - 0.5f >= other.transform.position.y + otherCollider.center.y + other.transform.localScale.y*otherCollider.size.y/2)
        //			if ( -0.01f <= accelation_y && accelation_y <= 0.01f )
        //				isGrounded = true;
        //	}
    }

    void OnCollisionExit2D(Collision2D other)
    {
        //	if (other.transform.tag == "Ground") {
        //			isGrounded = false;
        //		}
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.tag == "RopeObject" && (haveRope == false))
        {
            haveRope = true;
            GameObject.Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Ground")
        {

            walkVelocity = walkVelocity_init;
            if (Input.GetKey(KeyCode.LeftControl))
            { //&& isGrounded ){
                walkVelocity = walkVelocity_init * jumpBoost_init;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);
                //isGrounded = false;
            }
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.transform.tag == "RopeObject" && (haveRope == false))
        {
            haveRope = true;
            GameObject.Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Ground")
        {

            walkVelocity = walkVelocity_init;
            if (Input.GetKey(KeyCode.LeftControl))
            { //&& isGrounded ){
                walkVelocity = walkVelocity_init * jumpBoost_init;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);
                //isGrounded = false;

                audio.PlayOneShot(jumpAudioClip);
            }
        }

    }

    public void resetRope()
    {
        haveRope = false;
    }

    public bool isRope()
    {
        return haveRope;
    }

    public bool isSnake()
    {
        return haveSnake;
    }

    void getSnake()
    {
        haveSnake = true;

        changeAllRopeAnimationToSnakeAnimation();
    }

    void changeAllRopeAnimationToSnakeAnimation()
    {

        foreach (var r in GameObject.FindGameObjectsWithTag("RopeObject"))
        {

            r.GetComponent<Animator>().SetInteger("ropestate", 1);
            r.AddComponent<Bear>();

            r.GetComponent<BoxCollider2D>().isTrigger = false;
        }

    }

    void setClimb()
    {
        collider2D.enabled = false;
        isClimbing = true;
        rigidbody2D.gravityScale = 0.0f;
        GameObject.Find("PlayerMesh").GetComponent<Animator>().SetInteger("state", 2);
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    void resetClimb()
    {
        isClimbing = false;
        collider2D.enabled = true;
        rigidbody2D.gravityScale = gravityScale_init;
        GameObject.Find("PlayerMesh").GetComponent<Animator>().SetInteger("state", 0);
    }

    void Back(bool left)
    {
        isKnockBack = 0;
        if (left)
        {
            transform.rigidbody2D.velocity = new Vector2(-jumpVelocity, transform.rigidbody2D.velocity.y);
        }
        else
        {
            transform.rigidbody2D.velocity = new Vector2(+jumpVelocity, transform.rigidbody2D.velocity.y);
        }

        audio.PlayOneShot(knockbackAudioClip);
    }
}
