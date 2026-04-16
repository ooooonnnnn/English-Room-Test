using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterJump : CharacterControllerController, IAffectedByStats
{
    [SerializeField] private float jumpHeight;
    private bool isGrounded;
    private float verticalSpeed;
    
    public void HandleJumpInput(InputAction.CallbackContext ctx)
    {
        if (!isGrounded) return;

        if (ctx.ReadValueAsButton())
        {
            verticalSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
        }
    }
    
    private void FixedUpdate()
    {
        verticalSpeed += Physics.gravity.y * Time.fixedDeltaTime;
        characterController.Move(Vector3.up * (verticalSpeed * Time.fixedDeltaTime));
        
        if (isGrounded && verticalSpeed <= 0)
        {
            verticalSpeed = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }

    public void OnStatsChanged()
    {
        jumpHeight = Mathf.Clamp(
            PlayerStatsManager.Instance.GetStatValue(StatType.JumpHeight),
            0, float.PositiveInfinity);
    }
}
