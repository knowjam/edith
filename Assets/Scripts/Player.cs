using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(BoxCollider2D))]
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
    public LayerMask groundLayerMask;
    private RaycastHit2D touchGround;
    private GameObject _playerMesh;
    public GameObject blanketObject;
    private bool blanketed;
    public float itemAttackRange = 5;
    public LayerMask itemAttackLayerMask;

    static readonly float joystickThreshold = 0.3f;

    bool moveLeft
    {
        get
        {
            return Input.GetKey(KeyCode.LeftArrow) || VirtualJoystickRegion.VJRnormals.x < -joystickThreshold;
        }
    }

    bool moveRight
    {
        get
        {
            return Input.GetKey(KeyCode.RightArrow) || VirtualJoystickRegion.VJRnormals.x > joystickThreshold;
        }
    }

    bool moveStopped
    {
        get
        {
            return (!moveLeft && !moveRight && (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)))
                || (Application.platform == RuntimePlatform.Android && Mathf.Abs(VirtualJoystickRegion.VJRnormals.x) < joystickThreshold);
        }
    }

    bool jump
    {
        get
        {
            return Input.GetKeyDown(KeyCode.LeftControl) || JumpButton.jumpButtonHit;
        }
    }

    [SerializeField]
    private bool _haveRope; // 현재 로프를 가지고 있는 지 판단한다.
    public bool haveRope
    {
        get
        {
            return _haveRope && playerMode == PlayerMode.Rope;
        }
    }

    [SerializeField]
    private bool _haveBlanket; // 현재 보자기를 가지고 있는 지 판단한다.
    public bool haveBlanket
    {
        get
        {
            return _haveBlanket && playerMode == PlayerMode.Rope;
        }
    }

    public Texture2D ropeGuiTex;    // 밧줄 아이템을 가지고 있을 때
    public Texture2D snakeGuiTex;   // 뱀채찍을 가지고 있을 때
    public Texture2D ghostGuiTex;   // 보자기를 가지고 있을 때

    public enum PlayerMode
    {
        NotDetermined,      // 플레이어가 밧줄로 쓸지 뱀채찍으로 쓸지 결정하지 않음
        Rope,               // 밧줄로 쓰기로 결정됨
        Snake,              // 뱀채찍으로 쓰기로 결정됨
    }
    public PlayerMode playerMode = PlayerMode.NotDetermined;
    public bool haveGhostEffect;

    BoxCollider2D boxCollider;

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

        isKnockBack = knockBackFrame + 1;
        walkVelocity = walkVelocity_init;
        //	accelation_y = 0.0f;
        //	last_velocity_y = transform.rigidbody2D.velocity.y;
        _playerMesh = GameObject.Find("PlayerMesh");

        boxCollider = GetComponent<BoxCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -100)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        var p = transform.position;
        var s = GetComponent<BoxCollider2D>().size;
        var c = GetComponent<BoxCollider2D>().center;

        var origin = p + new Vector3(c.x, c.y - s.y / 2);

        Debug.DrawRay(origin, Vector3.down);
        touchGround = Physics2D.Raycast(origin, Vector3.down, 0.5f, groundLayerMask);
        
        if (touchGround)
        {
            walkVelocity = walkVelocity_init;
            if (jump)
            {
                walkVelocity = walkVelocity_init * jumpBoost_init;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVelocity);

                audio.PlayOneShot(jumpAudioClip);

                ConditionalRevertFromBlanket();
            }

            if (touchGround.collider.gameObject.GetComponent<MovePlatform>())
            {
                //boxCollider.sharedMaterial.friction = 1.0f;
                //collider2D.enabled = false;
                //collider2D.enabled = true;
            }
            else
            {
                //boxCollider.sharedMaterial.friction = 0.0f;
                //collider2D.enabled = false;
                //collider2D.enabled = true;
            }

            boxCollider.sharedMaterial.friction = 1.0f;
            collider2D.enabled = false;
            collider2D.enabled = true;
            
        }
        else
        {
            boxCollider.sharedMaterial.friction = 0.0f;
            collider2D.enabled = false;
            collider2D.enabled = true;
        }

        if (isKnockBack <= knockBackFrame)
        {
            isKnockBack++;
            return;
        }

        if (isClimbing == true)
            return;

        if (moveLeft)
        {
            rigidbody2D.velocity = new Vector2(-walkVelocity, rigidbody2D.velocity.y);

            transform.rotation = Quaternion.Euler(0, 180, 0);

            _playerMesh.GetComponent<Animator>().SetInteger("state", 1);

            ConditionalRevertFromBlanket();
        }
        else if (moveRight)
        {
            rigidbody2D.velocity = new Vector2(walkVelocity, rigidbody2D.velocity.y);

            transform.rotation = Quaternion.Euler(0, 0, 0);

            _playerMesh.GetComponent<Animator>().SetInteger("state", 1);

            ConditionalRevertFromBlanket();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (playerMode == PlayerMode.Snake)
            {
                _playerMesh.GetComponent<Animator>().SetInteger("state", 4);
                transform.Find("PlayerAttackRope").GetComponent<Animator>().SetTrigger("Attack");
            }
            else
            {
                _playerMesh.GetComponent<Animator>().SetInteger("state", 3);
            }
            //audio.PlayOneShot(attackAudioClip);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            ConditionalChangeToBlanket();
        }
        else if (blanketed == false)
        {
            _playerMesh.SetActive(true);
            _playerMesh.GetComponent<Animator>().SetInteger("state", 0);
        }

        if (moveStopped)
        {
            rigidbody2D.velocity = new Vector2(0.0f, rigidbody2D.velocity.y);
        }
    }

    private void ConditionalRevertFromBlanket()
    {
        // 이미 보자기 형태로 변신한 상태라면 원래 모습으로 돌아온다.
        if (blanketed)
        {
            blanketed = false;
            blanketObject.SetActive(false);
            _playerMesh.SetActive(true);
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    private void ConditionalChangeToBlanket()
    {
        // 보자기 아이템을 습득했다면 보자기 형태로 변신한다.
        if (_haveBlanket)
        {
            _playerMesh.SetActive(false);
            blanketed = true;
            blanketObject.SetActive(true);
            _haveBlanket = false;
            gameObject.layer = LayerMask.NameToLayer("PlayerInvincible");
        }
        else
        {
            ConditionalRevertFromBlanket();
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

    bool climbUpKeyPressed
    {
        get
        {
            return Input.GetKeyDown(KeyCode.UpArrow) || VirtualJoystickRegion.VJRnormals.y > 0.3f;
        }
    }

    bool climbDownKeyPressed
    {
        get
        {
            return Input.GetKeyDown(KeyCode.DownArrow) || VirtualJoystickRegion.VJRnormals.y < -0.3f;
        }
    }

    void Climb()
    {
        if (isClimbing == false)
            setClimb();

        rigidbody2D.velocity = new Vector2(0.0f, 0.0f);
        if (climbUpKeyPressed)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, walkVelocity);
        }
        else if (climbDownKeyPressed)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -walkVelocity);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        ConditionalLootRope(other);

        ConditionalLootGhost(other);
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

    void getGhost()
    {
        if (playerMode != PlayerMode.NotDetermined)
        {
            throw new Exception("getGhost() method should be called only when the player mode is not determined.");
        }

        playerMode = PlayerMode.Snake;
        ++killCount;

        haveGhostEffect = true;

        changeAllRopesToSnakes();
        changeAllGhostsToGhosts();
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

    void changeAllGhostsToGhosts()
    {
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

    void Back(KnockbackInfo info)
    {
        if (haveGhostEffect)
        {
            info.pusher.SendMessage("Back", new KnockbackInfo { pusher = this, left = info.pusher.gameObject.transform.position.x < transform.position.x });
            Destroy(info.pusher.gameObject, 2.0f);
            return;
        }

        isKnockBack = 0;
        if (info.left)
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
        else if (haveBlanket)
        {
            GUI.DrawTexture(new Rect(100, 0, 100, 100), ghostGuiTex);
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

    void ConditionalLootGhost(Collider2D other)
    {
        if (other.transform.tag == "GhostObject"
            && haveBlanket == false
            && playerMode != PlayerMode.Snake)
        {
            _haveBlanket = true;
            playerMode = PlayerMode.Rope;

            GameObject.Destroy(other.gameObject);
        }
    }

    public Vector3 itemAttackEndpoint { get { return transform.position + transform.right * itemAttackRange; } }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, itemAttackEndpoint);
    }

    public GameObject GetItemAttackRayCheckResult()
    {
        var result = Physics2D.Raycast(transform.position, transform.right, itemAttackRange, itemAttackLayerMask);
        return result.collider ? result.collider.gameObject : null;
    }
}
