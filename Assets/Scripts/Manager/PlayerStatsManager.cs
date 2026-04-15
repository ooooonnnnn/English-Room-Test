using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

[Tooltip("Keeps track of the player's stats")]
public class PlayerStatsManager : PersistentSingleton
{
    [SerializeField] private SerializedDictionary<StatType, float> stats;
    public UnityEvent onStatsChanged;

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
