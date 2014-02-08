using UnityEngine;
using System.Collections;
using System;

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
    public GUIStyle killScoreStyle;
    public int killCount;
    public bool transferedObject; // 다른 레벨에서 넘어온 플레이어 (진짜 플레이어)

    [SerializeField]
    private bool _haveRope; // 현재 로프를 가지고 있는 지 판단한다.
    public bool haveRope
    {
        get
        {
            return _haveRope && playerMode == PlayerMode.Rope;
        }
    }

    public Texture2D ropeGuiTex;    // 밧줄 아이템을 가지고 있을 때
    public Texture2D snakeGuiTex;   // 뱀채찍을 가지고 있을 때
    public enum PlayerMode
    {
        NotDetermined,      // 플레이어가 밧줄로 쓸지 뱀채찍으로 쓸지 결정하지 않음
        Rope,               // 밧줄로 쓰기로 결정됨
        Snake,              // 뱀채찍으로 쓰기로 결정됨
    }
    public PlayerMode playerMode = PlayerMode.NotDetermined;

    // Use this for initialization
    void Start()
    {
        var spDebug = GameObject.Find("StartPositionDebug");
        if (Application.isEditor && spDebug)
        {
            transform.position = spDebug.transform.position;
        }

        isClimbing = false;
        rigidbody2D.gravityScale = gravityScale_init;

        _haveRope = false;

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

    void OnLevelWasLoaded()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            if (!transferedObject)
            {
                Destroy(gameObject);
                return;
            }
        }

        var sp = GameObject.Find("StartPosition");
        if (sp)
        {
            transform.position = sp.transform.position;
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

        //if (other.transform.tag == "RopeObject" && (haveRope == false))
        //{
        //    AcquireRope();
        //    GameObject.Destroy(other.gameObject);
        //}

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
        ConditionalLootRope(other);

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

        //if (other.transform.tag == "RopeObject" && (haveRope == false))
        //{
        //    AcquireRope();
        //    GameObject.Destroy(other.gameObject);
        //}

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
        _haveRope = false;
    }

    void getSnake()
    {
        if (playerMode != PlayerMode.NotDetermined)
        {
            throw new Exception("getSnake() method should be called only when the player mode is not determined.");
        }

        playerMode = PlayerMode.Snake;
        ++killCount;
        
        changeAllRopesToSnakes();
    }

    void changeAllRopesToSnakes()
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

    void OnGUI()
    {
        if (haveRope)
        {
            GUI.DrawTexture(new Rect(0, 0, 100, 100), ropeGuiTex);
        }
        else if (playerMode == PlayerMode.Snake)
        {
            GUI.DrawTexture(new Rect(0, 0, 100, 100), snakeGuiTex);
            GUI.Label(new Rect(0, 0, 100, 100), Convert.ToString(killCount), killScoreStyle);
        }
    }

    void ConditionalLootRope(Collider2D other)
    {
        if (other.transform.tag == "RopeObject"
            && haveRope == false
            && playerMode != PlayerMode.Snake)
        {
            _haveRope = true;
            playerMode = PlayerMode.Rope;

            GameObject.Destroy(other.gameObject);
        }
    }
}
