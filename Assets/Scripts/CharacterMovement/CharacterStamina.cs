using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStamina : MonoBehaviour, IAffectedByStats
{
    [SerializeField] private float maxStamina;
    [SerializeField, Tooltip("How much of the total stamina is regenerated per second?")]
    private float proportionalRegenRate;
    private float currentStamina;
    /// <summary>
    /// Passed with:
    /// - current stamina
    /// - ratio of stamina to max stamina
    /// </summary>
    public UnityEvent<float, float> onStaminaChanged;

    private void Start()
    {
        currentStamina = maxStamina;
        ReportStamina();
    }

    private void ReportStamina()
    {
        onStaminaChanged.Invoke(currentStamina, currentStamina / maxStamina);
    }

    private void FixedUpdate()
    {
        RegenerateStamina();
    }

    private void RegenerateStamina()
    {
        if (currentStamina >= maxStamina) return;
        
        currentStamina += proportionalRegenRate * maxStamina * Time.fixedDeltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        ReportStamina();
    }

    public void ConsumeStamina(float amount)
    {
        if (currentStamina <= 0) return;
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        ReportStamina();
    }

    public void HandleStatsChanged()
    {
        maxStamina = Mathf.Clamp(
            PlayerStatsManager.Instance.GetStatValue(StatType.MaxStamina),
            0, float.PositiveInfinity);
        ReportStamina();
    }
}
