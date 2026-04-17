using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

[Tooltip("Keeps track of the player's stats")]
public class PlayerStatsManager : PersistentSingleton<PlayerStatsManager>
{
    private Dictionary<StatType, float> _stats;
    [SerializeField] private List<StatType> statsDictKeys;
    [SerializeField] private List<float> statsDictValues;
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

    protected override void Awake()
    {
        base.Awake();
        _stats = new Dictionary<StatType, float>(statsDictKeys.Zip(statsDictValues,
            (key, val) => new KeyValuePair<StatType, float>(key, val)));
    }

    private void Start()
    {
        RecalculateStats();
    }

    public float GetStatValue(StatType stat)
    { 
        return _stats.GetValueOrDefault(stat, statDefaultValues.GetDefaultValue(stat));
    }

    public void SetStatValue(StatType stat, float value)
    {
        _stats[stat] = value;
        onStatsChanged.Invoke();
    }

    [ContextMenu("Initialize with default values")]
    private void AddAllStatsDefaultValues()
    {
        statsDictKeys = new();
        statsDictValues = new();
        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            statsDictKeys.Add(statType);
            statsDictValues.Add(statDefaultValues.GetDefaultValue(statType));
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
                _stats[statEffect.StatType] += statEffect.Value;
            }
        }
        onStatsChanged.Invoke();
    }
}
