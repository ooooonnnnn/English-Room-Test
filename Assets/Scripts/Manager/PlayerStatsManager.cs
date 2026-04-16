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
    [SerializeField] private StatType_DefaultValues statDefaultValues;
    public UnityEvent onStatsChanged;

    [SerializeField] private InputActionReference debugAction;

    #region Debug

    private void OnValidate()
    {
        //Debug action
        debugAction.action.performed += ctx => ChangeRandomStat();
    }

    private void ChangeRandomStat()
    {
        StatType[] statTypes = (StatType[])Enum.GetValues(typeof(StatType));
        SetStatValue(statTypes[Random.Range(0, statTypes.Length)], Random.Range(0, 100));
    }

    #endregion

    public float GetStatValue(StatType stat)
    { 
        return stats.GetValueOrDefault(stat, statDefaultValues.GetDefaultValue(stat));
    }

    public void SetStatValue(StatType stat, float value)
    {
        stats[stat] = value;
        onStatsChanged.Invoke();
    }

    [ContextMenu("Initialize with default values")]
    private void AddAllStatsDefaultValues()
    {
        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            SetStatValue(statType, statDefaultValues.GetDefaultValue(statType));
        }
    }

    public void RecalculateStats()
    {
        //Reset to default values
        AddAllStatsDefaultValues();
        
        //Add stat effects from equipped gear
        var equippedGear= PlayerGearManager.Instance.GetEquippedGear();
        foreach (var gear in equippedGear)
        {
            var statEffects = gear.ItemData.StatAffectorData;
            foreach (var statEffect in statEffects)
            {
                stats[statEffect.StatType] += statEffect.Value;
            }
        }
        onStatsChanged.Invoke();
    }
}
