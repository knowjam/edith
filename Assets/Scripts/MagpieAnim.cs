using UnityEngine;
using System.Collections;

public class MagpieAnim : MonoBehaviour
{
    Animator animator;
    Vector3 prevPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        prevPosition = transform.position;

    }

    void Update()
    {
        var positionDiff = transform.position - prevPosition;

        prevPosition = transform.position;
        animator.SetFloat("MoveSpeed", positionDiff.magnitude * Time.deltaTime * 100);

    }
}
