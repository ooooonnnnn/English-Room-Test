using System;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "StatType_DefaultValues", menuName = "Scriptable Objects/StatType_DefaultValues")]
public class StatType_DefaultValues : ScriptableObject
{
    [SerializeField] private SerializedDictionary<StatType, float> statTypeDefaultValues;
    
    public float GetDefaultValue(StatType statType) => statTypeDefaultValues[statType];

    [ContextMenu("Add missing keys")]
    private void AddMissingKeys()
    {
        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            statTypeDefaultValues.TryAdd(statType, 0);
        }
    }
}
