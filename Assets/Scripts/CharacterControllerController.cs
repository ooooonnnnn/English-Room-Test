using UnityEngine;

/// <summary>
/// A component that sends commands to a CharacterController
/// </summary>
[RequireComponent(typeof(CharacterController))]
public abstract class CharacterControllerController : MonoBehaviour
{
    [SerializeField, HideInInspector] protected CharacterController characterController;
    
    private void OnValidate()
    {
        characterController = GetComponent<CharacterController>();
    }
}
