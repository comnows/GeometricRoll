using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerData playerData;
    private Rigidbody2D rb2D;

    [SerializeField] private Transform feetTransform;
    [SerializeField] private float groundCheckRadius = 1f;
    [SerializeField] private Vector2 checkBoxSize = new Vector2(0.1f, 0.1f);
    [SerializeField] private LayerMask groundLayerMask;
    // [SerializeField] private float jumpForce = 5f;
    private bool isTop = false;
    private bool isJump = false;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerData = GetComponent<PlayerData>();
    }

    private void Start()
    {
        SetGravityScale(playerData.gravityScale);
    }

    private void Update()
    {
        playerData.currentJumpBufferTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerData.currentJumpBufferTime = playerData.jumpBufferTime;
            // if (IsGround())
            // {
            //     isJump = true;
            // }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ChangeGravity();
            RotatePlayer();
        }
    }

    private void FixedUpdate()
    {
        if (playerData.currentJumpBufferTime > 0 && IsGround())
        {
            Jump();
        }

        // if (isJump)
        // {
        //     Jump();
        //     isJump = false;
        // }
    }

    private void SetGravityScale(float gravityScale)
    {
        rb2D.gravityScale = gravityScale;
    }

    private bool IsGround()
    {
        return Physics2D.OverlapBox(feetTransform.position, checkBoxSize, 0, groundLayerMask);
    }

    private bool CanJump()
    {
        return IsGround() && true;
    }

    private void Jump()
    {
        rb2D.AddForce(transform.up * GetJumpForce(), ForceMode2D.Impulse);
    }

    private float GetJumpForce()
    {
        float jumpForce = playerData.jumpForce;

        if (rb2D.velocity.y < 0)
        {
            jumpForce -= rb2D.velocity.y;
        }

        return jumpForce;
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
        Gizmos.DrawWireCube(feetTransform.position, checkBoxSize);
    }
}
