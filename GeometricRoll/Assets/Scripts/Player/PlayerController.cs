using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private Transform feetTransform;
    [SerializeField] private float groundCheckRadius = 1f;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float jumpForce = 5f;
    private bool isTop = false;
    private bool isJump = false;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (IsGround())
            {
                isJump = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ChangeGravity();
            RotatePlayer();
        }
    }

    private void FixedUpdate()
    {
        if (isJump)
        {
            Jump();
            isJump = false;
        }
    }

    private bool IsGround()
    {
        return Physics2D.OverlapCircle(feetTransform.position, groundCheckRadius, groundLayerMask);
    }

    private void Jump()
    {
        rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void ChangeGravity()
    {
        rb2D.gravityScale *= -1;
    }

    private void RotatePlayer()
    {
        if (!isTop)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        isTop = !isTop;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(feetTransform.position, groundCheckRadius);
    }
}
