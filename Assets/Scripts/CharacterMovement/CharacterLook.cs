using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Rotates the character left and right, and the camera up and down
/// </summary>
public class CharacterLook : CharacterControllerController
{
    [SerializeField] private Transform cameraOrigin;
    [SerializeField] private float sensitivity;
    [SerializeField] private float minLookAngle, maxLookAngle;
    [SerializeField] private bool invertY;
    
    public void HandleLookInput(InputAction.CallbackContext ctx)
    {
        var input = ctx.ReadValue<Vector2>();
        var lookAmount = input * sensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up, lookAmount.x);

        ClampedCameraPitch(lookAmount.y * (invertY ? -1 : 1));
    }

    private void ClampedCameraPitch(float inputY)
    {
        var currentCamRotation = cameraOrigin.localRotation.eulerAngles;
        var currentAngle = currentCamRotation.x;
        var newAngle = Mathf.Clamp(currentAngle + inputY, minLookAngle, maxLookAngle);
        cameraOrigin.localRotation = Quaternion.Euler(newAngle, currentCamRotation.y, currentCamRotation.z);
    }
}
