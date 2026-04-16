using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterMove : CharacterControllerController, IAffectedByStats, IRequireStamina
{
    [SerializeField] private float speed;
    [SerializeField, Tooltip("Sprinting speed is walking speed times this factor")]
    private float runSpeedFactor;
    private bool _canRun = true;
    public bool CanRun 
    {
        get => _canRun;
        set
        {
            _canRun = value;
            if (!value) _isRunning = false;
        }
    }
    [SerializeField] private float minStaminaToSprint;
    [SerializeField] private float sprintStaminaCostPerSecond;
    public float StaminaCostPerFixedUpdate => sprintStaminaCostPerSecond * Time.fixedDeltaTime;
    /// <summary>
    /// Passed with the amount of stamina used to sprint in the last fixed update.
    /// </summary>
    public UnityEvent<float> onSprint;
    
    private bool _isRunning;
    private Vector2 _inputDir;

    /// <summary>
    /// Takes a vector2 input and moves the character in the direction of the vector
    /// </summary>
    /// <param name="ctx"></param>
    public void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        _inputDir = ctx.ReadValue<Vector2>();
    }

    /// <summary>
    /// Takes a button input and sets running mode
    /// </summary>
    /// <param name="ctx"></param>
    public void HandleRunInput(InputAction.CallbackContext ctx)
    {
        var runInput = ctx.ReadValueAsButton();
        _isRunning = runInput && _canRun;
    }

    private void FixedUpdate()
    {
        var totalSpeed = _isRunning ? speed * runSpeedFactor : speed; 
        characterController.Move((transform.forward * _inputDir.y + transform.right * _inputDir.x)
                                 * (totalSpeed * Time.fixedDeltaTime));
        
        if (_isRunning && _inputDir.sqrMagnitude > 0) 
            onSprint.Invoke(StaminaCostPerFixedUpdate);
    }

    public void HandleStatsChanged()
    {
        speed = Mathf.Clamp(
            PlayerStatsManager.Instance.GetStatValue(StatType.MoveSpeed),
            0, float.PositiveInfinity);
    }

    public void HandleStaminaChange(float currentStamina)
    {
        if (!_isRunning)
            CanRun = currentStamina >= minStaminaToSprint;
        else
            CanRun = currentStamina > 0;
    }
}