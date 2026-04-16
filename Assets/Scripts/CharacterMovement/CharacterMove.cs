using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMove : CharacterControllerController, IAffectedByStats
{
    [SerializeField] private float speed;
    [SerializeField] private float runSpeedFactor;

    private bool _canRun = true;
    public bool canRun { 
        get => _canRun;
        set
        {
            _canRun = value;
            if (!value) _isRunning = false;
        }
    }
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
        _isRunning = runInput && canRun;
    }

    private void FixedUpdate()
    {
        var totalSpeed = _isRunning ? speed * runSpeedFactor : speed; 
        characterController.Move((transform.forward * _inputDir.y + transform.right * _inputDir.x)
                                 * (totalSpeed * Time.fixedDeltaTime));
    }

    public void OnStatsChanged()
    {
        speed = Mathf.Clamp(
            PlayerStatsManager.Instance.GetStatValue(StatType.MoveSpeed),
            0, float.PositiveInfinity);
    }
}