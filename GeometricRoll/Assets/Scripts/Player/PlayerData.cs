using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public new Rigidbody2D rigidbody2D;

    [Header("Gravity")]
    [ReadOnly] public float gravityStrength;
    [ReadOnly] public float gravityScale;

    [Header("Jump")]
    public float jumpHeight = 1;
    public float jumpTimeToApex = 0.75f;
    public float jumpForce;

    public float jumpBufferTime = 0.1f;
    public float currentJumpBufferTime = 0f;

    [Header("Ground Checker")]
    public Transform groundCheckTransform;
    [Range(0.01f, 0.5f)] public float additionCheckBoxSize = 0.05f;
    [ReadOnly] public Vector2 checkBoxSize;
    public LayerMask groundLayerMask;

    public Transform spriteTransform;
    [ReadOnly] public bool isGravityFlip = false;

    [Range(1f, 20f)] public float lerpSpeed;
    [ReadOnly] public float lerpTime = 0f;
    [ReadOnly] public Vector3 startSpriteRotation = Vector3.zero;

    private void OnValidate()
    {
        if (rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
        gravityScale = gravityStrength / Physics2D.gravity.y;

        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

        if (spriteTransform != null)
        {
            checkBoxSize = new Vector2(spriteTransform.localScale.x + additionCheckBoxSize, additionCheckBoxSize);
        }
    }
}
