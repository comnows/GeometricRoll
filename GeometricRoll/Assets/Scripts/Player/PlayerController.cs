using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

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

        RotateSprite();
    }

    private float GetNegateValue(float value)
    {
        return value *= -1;
    }

    private void RotatePlayer()
    {
        if (!playerData.isGravityFlip)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        playerData.isGravityFlip = !playerData.isGravityFlip;
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
        return Physics2D.OverlapBox(playerData.groundCheckTransform.position, playerData.checkBoxSize, 0f, playerData.groundLayerMask);
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

    private void RotateSprite()
    {
        if (IsGround())
        {
            if (playerData.spriteTransform.rotation.eulerAngles.z % 90 == 0) { return; }

            Vector3 rotation = playerData.startSpriteRotation;
            rotation.z = Mathf.Round(rotation.z / 90) * 90;

            playerData.spriteTransform.rotation = Quaternion.Slerp(Quaternion.Euler(playerData.startSpriteRotation), Quaternion.Euler(rotation), playerData.lerpTime);
            playerData.lerpTime += Time.deltaTime * playerData.lerpSpeed;
        }
        else
        {
            playerData.spriteTransform.Rotate(Vector3.back * 180 / (playerData.jumpTimeToApex * 2) * Time.deltaTime);

            playerData.lerpTime = 0;
            playerData.startSpriteRotation = playerData.spriteTransform.rotation.eulerAngles;
        }
    }

    public Vector2 size = new Vector2(1.1f, 1.1f);


    private void OnDrawGizmos()
    {
        // Gizmos.matrix = playerData.playerSpriteTransform.localToWorldMatrix;
        Gizmos.color = Color.blue;
        // Gizmos.DrawWireCube(Vector3.zero, playerData.checkBoxSize);
        float rotationAngle = transform.rotation.eulerAngles.z;

        // Convert the rotation angle to radians
        float angleRad = rotationAngle * Mathf.Deg2Rad;

        // Calculate the half size of the square
        Vector2 halfSize = size * 0.5f;

        // Calculate the rotated corner points of the square
        Vector2 topLeft = transform.position + new Vector3(Mathf.Cos(angleRad) * -halfSize.x - Mathf.Sin(angleRad) * halfSize.y,
                                                          Mathf.Sin(angleRad) * -halfSize.x + Mathf.Cos(angleRad) * halfSize.y);
        Vector2 topRight = transform.position + new Vector3(Mathf.Cos(angleRad) * halfSize.x - Mathf.Sin(angleRad) * halfSize.y,
                                                           Mathf.Sin(angleRad) * halfSize.x + Mathf.Cos(angleRad) * halfSize.y);
        Vector2 bottomLeft = transform.position + new Vector3(Mathf.Cos(angleRad) * -halfSize.x - Mathf.Sin(angleRad) * -halfSize.y,
                                                             Mathf.Sin(angleRad) * -halfSize.x + Mathf.Cos(angleRad) * -halfSize.y);
        Vector2 bottomRight = transform.position + new Vector3(Mathf.Cos(angleRad) * halfSize.x - Mathf.Sin(angleRad) * -halfSize.y,
                                                              Mathf.Sin(angleRad) * halfSize.x + Mathf.Cos(angleRad) * -halfSize.y);

        // Draw the rotated square using Gizmos.DrawLine
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
