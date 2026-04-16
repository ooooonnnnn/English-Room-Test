using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float runSpeedFactor;
    public bool canRun { 
        get => _canRun;
        set
        {
            _canRun = value;
            if (!value) _isRunning = false;
        }
    }

    private bool _canRun = true;
    private bool _isRunning;
    
    [SerializeField, HideInInspector] private CharacterController characterController;
    private Vector2 _inputDir;

    private void OnValidate()
    {
        characterController = GetComponent<CharacterController>();
    }

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
        characterController.Move(new Vector3(_inputDir.x, 0, _inputDir.y) * (totalSpeed * Time.fixedDeltaTime));
    }
}