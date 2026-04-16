using UnityEngine;

/// <summary>
/// For components that represent actions that require stamina
/// </summary>
public interface IRequireStamina
{
    // /// <summary>
    // /// Can the action be performed? This get set flase when there's not enough stamina.
    // /// </summary>
    // public bool actionAllowed { get; set; }
    //
    // public float minStaminaRequired { get; }
    //
    // public float staminaCostPerAction { get; }

    public void HandleStaminaChange(float currentStamina);
}
