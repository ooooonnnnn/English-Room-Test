using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterJump : CharacterControllerController, IAffectedByStats, IRequireStamina
{
    [SerializeField] private float jumpHeight;
    private bool isGrounded;
    private float verticalSpeed;
    private bool _canJump = true;
    [SerializeField] private float staminaCost;
    /// <summary>
    /// Passed with the amount of stamina used to jump.
    /// </summary>
    public UnityEvent<float> onJump;
    
    public void HandleJumpInput(InputAction.CallbackContext ctx)
    {
        if (!isGrounded || !_canJump) return;

        if (ctx.ReadValueAsButton())
        {
            verticalSpeed = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
            onJump.Invoke(staminaCost);
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

    public void HandleStatsChanged()
    {
        jumpHeight = Mathf.Clamp(
            PlayerStatsManager.Instance.GetStatValue(StatType.JumpHeight),
            0, float.PositiveInfinity);
    }

    public void HandleStaminaChange(float currentStamina, float _)
    {
        _canJump = currentStamina >= staminaCost;
    }
}
