using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Gravity")]
    [ReadOnly] public float gravityStrength;
    [ReadOnly] public float gravityScale;

    [Header("Jump")]
    public float jumpHeight = 1;
    public float jumpTimeToApex = 0.75f;
    public float jumpForce;

    public float jumpBufferTime = 0.1f;
    public float currentJumpBufferTime = 0f;

    private void OnValidate()
    {
        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
        gravityScale = gravityStrength / Physics2D.gravity.y;

        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;
    }
}
