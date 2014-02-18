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
    public LayerMask collisionMask;
    private GameObject _playerSnakeMesh;
    private GameObject _playerMesh;
    private bool touchGround;

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

        _playerMesh = GameObject.Find("PlayerMesh");

        _playerSnakeMesh = GameObject.Find("PlayerSnakeMesh");
        if (_playerSnakeMesh)
        {
            _playerSnakeMesh.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        var p = transform.position;
        var s = GetComponent<BoxCollider2D>().size;
        var c = GetComponent<BoxCollider2D>().center;

        var origin = p + new Vector3(c.x, c.y - s.y / 2);

        Debug.DrawRay(origin, Vector3.down);
        touchGround = Physics2D.Raycast(origin, Vector3.down, 0.5f, collisionMask);
        if (touchGround)
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

        if (transform.FindChild("TopSensor").GetComponent<SimpleTriggerSensor>().triggered)
        {
            if (!GetComponent<BoxCollider2D>().isTrigger)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                transform.FindChild("TopSensor").GetComponent<SimpleTriggerSensor>().enabled = false;
                GetComponents<BoxCollider2D>()[1].enabled = false;
            }

            transform.FindChild("TopSensor").GetComponent<SimpleTriggerSensor>().triggered = false;
        }

        if (transform.FindChild("BottomSensor").GetComponent<SimpleTriggerSensor>().triggered)
        {
            if (!GetComponent<BoxCollider2D>().isTrigger)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                transform.FindChild("TopSensor").GetComponent<SimpleTriggerSensor>().enabled = true;
                GetComponents<BoxCollider2D>()[1].enabled = true;
            }

            transform.FindChild("BottomSensor").GetComponent<SimpleTriggerSensor>().triggered = false;
        }

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

            _playerMesh.GetComponent<Animator>().SetInteger("state", 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody2D.velocity = new Vector2(walkVelocity, rigidbody2D.velocity.y);

            transform.rotation = Quaternion.Euler(0, 0, 0);

            _playerMesh.GetComponent<Animator>().SetInteger("state", 1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //_playerMesh.GetComponent<Animator>().SetInteger("state", 3);

            _playerMesh.SetActive(false);
            _playerSnakeMesh.SetActive(true);
            
            //audio.PlayOneShot(attackAudioClip);
        }
        else
        {
            _playerMesh.SetActive(true);
            _playerMesh.GetComponent<Animator>().SetInteger("state", 0);
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

    public void SwitchToNormalMesh()
    {
        _playerMesh.SetActive(true);
        _playerSnakeMesh.SetActive(false);
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


    void OnTriggerEnter2D(Collider2D other)
    {
        ConditionalLootRope(other);

        //if (other.gameObject.tag == "Ground")
        //{

        //    walkVelocity = walkVelocity_init;
        //    if (Input.GetKey(KeyCode.LeftControl))
        //    { //&& isGrounded ){
        //        walkVelocity = walkVelocity_init * jumpBoost_init;
        //        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);
        //        //isGrounded = false;
        //    }
        //}

    }

    void OnTriggerStay2D(Collider2D other)
    {
        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        
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
        _playerMesh.GetComponent<Animator>().SetInteger("state", 2);
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    void resetClimb()
    {
        isClimbing = false;
        collider2D.enabled = true;
        rigidbody2D.gravityScale = gravityScale_init;
        _playerMesh.GetComponent<Animator>().SetInteger("state", 0);
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
