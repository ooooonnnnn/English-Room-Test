using System;
using UnityEngine;

/// <summary>
/// Contains data to affect a stat by adding or subtracting a value
/// </summary>
[Serializable]
public class StatAffectorData
{
    [SerializeField] private StatType statType;
    [SerializeField] private float value;
    
    /// <summary>
    /// The stat to affect.
    /// </summary>
    public StatType StatType => statType;
    /// <summary>
    /// The amount to add to the stat
    /// </summary>
    public float Value => value;
}
