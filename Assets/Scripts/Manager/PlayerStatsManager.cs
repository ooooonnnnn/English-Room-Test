using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

[Tooltip("Keeps track of the player's stats")]
public class PlayerStatsManager : PersistentSingleton<PlayerStatsManager>
{
    [SerializeField] private SerializedDictionary<StatType, float> stats;
    public UnityEvent onStatsChanged;

    [SerializeField] private InputActionReference debugAction;

    private void OnValidate()
    {
        debugAction.action.performed += ctx => ChangeRandomStat();
    }

    private void ChangeRandomStat()
    {
        StatType[] statTypes = (StatType[])Enum.GetValues(typeof(StatType));
        SetStatValue(statTypes[Random.Range(0, statTypes.Length)], Random.Range(0, 100));
    }

    public float GetStatValue(StatType stat)
    { 
        //TODO: default values SO
        return stats.GetValueOrDefault(stat, 0);
    }

    public void SetStatValue(StatType stat, float value)
    {
        stats[stat] = value;
        onStatsChanged?.Invoke();
    }

    [ContextMenu("Initialize with default values")]
    private void AddAllStatsDefaultValues()
    {
        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            //TODO: default values SO
            SetStatValue(statType, 0);
        }
    }
}
