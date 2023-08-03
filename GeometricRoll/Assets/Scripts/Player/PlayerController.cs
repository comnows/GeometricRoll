using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerData playerData;

    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
    }

    private void Start()
    {
        SetGravityScale(playerData.gravityScale);
    }

    private void SetGravityScale(float gravityScale)
    {
        playerData.rigidbody2D.gravityScale = gravityScale;
    }

    private void Update()
    {
        playerData.currentJumpBufferTime -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            playerData.currentJumpBufferTime = playerData.jumpBufferTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SetGravityScale(GetNegateValue(playerData.rigidbody2D.gravityScale));
            RotatePlayer();
        }
    }

    private float GetNegateValue(float value)
    {
        return value *= -1;
    }

    private void RotatePlayer()
    {
        if (!playerData.isTop)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        playerData.isTop = !playerData.isTop;
    }

    private void FixedUpdate()
    {
        if (CanJump())
        {
            Jump();
        }
    }

    private bool CanJump()
    {
        return IsGround() && playerData.currentJumpBufferTime > 0;
    }

    private bool IsGround()
    {
        return Physics2D.OverlapBox(playerData.feetTransform.position, playerData.checkBoxSize, 0, playerData.groundLayerMask);
    }

    private void Jump()
    {
        playerData.rigidbody2D.AddForce(transform.up * GetJumpForce(), ForceMode2D.Impulse);
    }

    private float GetJumpForce()
    {
        float jumpForce = playerData.jumpForce;

        if (playerData.rigidbody2D.velocity.y < 0)
        {
            jumpForce -= playerData.rigidbody2D.velocity.y;
        }

        return jumpForce;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(playerData.feetTransform.position, playerData.checkBoxSize);
    }
}
