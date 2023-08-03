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
    public Transform feetTransform;
    public Vector2 checkBoxSize = new Vector2(0.1f, 0.1f);
    public LayerMask groundLayerMask;

    [ReadOnly] public bool isTop = false;

    private void OnValidate()
    {
        if (rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
        gravityScale = gravityStrength / Physics2D.gravity.y;

        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;
    }
}
