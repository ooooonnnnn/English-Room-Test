using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class StatsHUD : MonoBehaviour
{
    [SerializeField] private Transform statsContainer;
    [SerializeField] private SerializedDictionary<StatType, TMP_Text> statTextDict;
    [SerializeField] private StatType_Names statTypeNames;
    [SerializeField] private TMP_Text statTextPrefab;

    private void Start()
    {
        UpdateStatValues();
    }

    [ContextMenu("Initialize stats container")]
    private void InitializeStatsContainer()
    {
        ClearStatsContainer();
        PopulateStatsContainer();
    }

    private void ClearStatsContainer()
    {
        var childrenToDestroy = new List<Transform>();
        foreach (Transform child in statsContainer)   
        {
            childrenToDestroy.Add(child);
        }
        print(childrenToDestroy.Count);
        foreach (var child in childrenToDestroy)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    private void PopulateStatsContainer()
    {
        statTextDict = new();
        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            TMP_Text newText = PrefabUtility.InstantiatePrefab(statTextPrefab, statsContainer) as TMP_Text;
            statTextDict.Add(statType, newText);
        }
    }

    public void UpdateStatValues()
    {
        foreach (var statType in statTextDict.Keys)
        {
            var value = PlayerStatsManager.Instance.GetStatValue(statType);
            statTextDict[statType].text =
                $"{statTypeNames.GetStatTypeName(statType)}: {value:N0}";
            //TODO: state type names in stat manager, not here
        }
    }
}
