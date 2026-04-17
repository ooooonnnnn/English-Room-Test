using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatType_Names", menuName = "Scriptable Objects/StatType_Names")]
public class StatType_Names : SingletonSO<StatType_Names>
{
    [SerializeField] private List<StatType> statTypeNamesDictKeys;
    [SerializeField] private List<string> statTypeNamesDictValues;

    #region Initialization

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
            
        ConditionalInitializeDict();
    }
#endif

        private void ConditionalInitializeDict()
        {
            statTypeNamesDictKeys ??= new();
            statTypeNamesDictValues ??= new();
        }

        [ContextMenu("Add missing Stat Names")]
        private void AddMissingStatNames()
        {
            foreach (var statType in Enum.GetValues(typeof(StatType)))
            {
                if (statTypeNamesDictKeys.Contains((StatType)statType)) continue;
                statTypeNamesDictKeys.Add((StatType)statType);
                statTypeNamesDictValues.Add(statType.ToString());
                // statTypeNamesDictValues[
                //     statTypeNamesDictKeys.FindIndex(x => x == (StatType)statType)] 
                //     = statType.ToString();
            }
        }
    #endregion

    public string GetStatTypeName(StatType statType) => 
        statTypeNamesDictValues[statTypeNamesDictKeys.FindIndex(x => x == statType)];
}
