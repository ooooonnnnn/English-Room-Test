using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleOnAction : MonoBehaviour
{
    [SerializeField] private InputActionReference inputAction;

    private void Awake()
    {
        inputAction.action.performed += ToggleActive;
    }

    public void ToggleActive(InputAction.CallbackContext ctx)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
