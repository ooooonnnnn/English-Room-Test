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
}
