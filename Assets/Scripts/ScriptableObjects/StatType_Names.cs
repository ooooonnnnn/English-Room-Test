using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "StatType_Names", menuName = "Scriptable Objects/StatType_Names")]
public class StatType_Names : ScriptableObject
{
    [SerializeField] private SerializedDictionary<StatType, string> statTypeNames;

    #region Initialization
        private void OnValidate()
        {
            ConditionalInitializeDict();

            AddMissingStatNames();
        }

        private void AddMissingStatNames()
        {
            foreach (var statType in Enum.GetValues(typeof(StatType)))
            {
                if (statTypeNames.ContainsKey((StatType)statType)) continue;
                statTypeNames.Add((StatType)statType, statType.ToString());
            }
        }

        private void ConditionalInitializeDict()
        {
            statTypeNames ??= new();
        }
    #endregion

    public string GetStatTypeName(StatType statType) => statTypeNames[statType];
}
