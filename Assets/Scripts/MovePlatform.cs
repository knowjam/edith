using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class MovePlatform : MonoBehaviour
{
    public bool leftRight = true; // true면 좌우이동, false면 상하이동
    public float moveAmount;
    public float moveVelocity;
    private float startPosition;
    private bool curMoveLeft;
    Vector3 initialPosition;
    public bool changeDirectionOnEnds;

    // Use this for initialization
    void Start()
    {
        ConditionalFlipDirection();

        initialPosition = transform.position;

        if (leftRight)
        {
            startPosition = transform.position.x;
            transform.rigidbody2D.velocity = new Vector2(moveVelocity, 0.0f);
        }
        else
        {
            startPosition = transform.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (curMoveLeft)
        {
            if (leftRight)
            {
                if (startPosition > transform.position.x)
                {
                    curMoveLeft = false;
                    transform.rigidbody2D.velocity = new Vector2(moveVelocity, 0.0f);
                    ConditionalFlipDirection();
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * moveVelocity, transform.position.z);

                if (startPosition > transform.position.y)
                {
                    curMoveLeft = false;
                    ConditionalFlipDirection();
                }
            }
        }
        else
        {
            if (leftRight)
            {
                if (startPosition + moveAmount < transform.position.x)
                {
                    curMoveLeft = true;
                    ConditionalFlipDirection();
                    transform.rigidbody2D.velocity = new Vector2(-moveVelocity, 0.0f);
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * moveVelocity, transform.position.z);

                if (startPosition + moveAmount < transform.position.y)
                {
                    curMoveLeft = true;
                    ConditionalFlipDirection();
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        var beginPoint = Application.isPlaying ? initialPosition : transform.position;
        if (leftRight)
        {
            beginPoint.y += 0.2f; // 오브젝트에 묻히지 않을 정도로 위로 밀기
        }
        beginPoint.z -= 0.1f; // 오브젝트에 묻히지 않을 정도로 카메라쪽으로 당기자
        var endPoint = beginPoint + (leftRight ? new Vector3(moveAmount, 0, 0) : new Vector3(0, moveAmount, 0));

        Gizmos.color = Color.yellow;
        var arrowSize = 0.2f;
        Gizmos.DrawLine(beginPoint, endPoint);
        Gizmos.DrawLine(beginPoint, beginPoint + (leftRight ? new Vector3(arrowSize, arrowSize, 0) : new Vector3(-arrowSize, arrowSize, 0)));
        Gizmos.DrawLine(beginPoint, beginPoint + (leftRight ? new Vector3(arrowSize, -arrowSize, 0) : new Vector3(arrowSize, arrowSize, 0)));
        Gizmos.DrawLine(endPoint, endPoint + (leftRight ? new Vector3(-arrowSize, arrowSize, 0) : new Vector3(-arrowSize, -arrowSize, 0)));
        Gizmos.DrawLine(endPoint, endPoint + (leftRight ? new Vector3(-arrowSize, -arrowSize, 0) : new Vector3(arrowSize, -arrowSize, 0)));
    }

    void ConditionalFlipDirection()
    {
        if (changeDirectionOnEnds && leftRight)
        {
            transform.Rotate(0, 180, 0);
        }
    }
}
