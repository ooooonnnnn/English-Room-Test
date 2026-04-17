using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class StatsHUD : MonoBehaviour
{
    [SerializeField] private Transform statsContainer;
    [SerializeField, HideInInspector] private StatType_Names statTypeNames;
    [SerializeField] private TMP_Text statTextPrefab;
    private Dictionary<StatType, TMP_Text> _statTextDict;
    [SerializeField] private List<StatType> statTextDictKeys;
    [SerializeField] private List<TMP_Text> statTextDictValues;

    #if UNITY_EDITOR
    private void OnValidate()
    {
        statTypeNames = StatType_Names.LoadSingletonAsset();
    }
    #endif

    private void Awake()
    {
        if (statTextDictKeys == null || statTextDictValues == null)
        {
            _statTextDict = new();
            statTextDictKeys = new();
            statTextDictValues = new();
        }
        else
        {
            _statTextDict = new(
                statTextDictKeys.Zip(statTextDictValues,
                    (key, val) => new KeyValuePair<StatType, TMP_Text>(key, val)));
        }
    }

    private void Start()
    {
        UpdateStatValues();
    }

    [ContextMenu("Initialize stats container")]
    private void InitializeStatsContainer()
    {
        ClearStatsContainer();
        #if UNITY_EDITOR
        PopulateStatsContainer();
        #endif
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

#if UNITY_EDITOR
    private void PopulateStatsContainer()
    {
        _statTextDict = new();
        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            TMP_Text newText = PrefabUtility.InstantiatePrefab(statTextPrefab, statsContainer) as TMP_Text;
            _statTextDict.Add(statType, newText);
        }
        statTextDictKeys = _statTextDict.Keys.ToList();
        statTextDictValues = _statTextDict.Values.ToList();
    }
#endif

    public void UpdateStatValues()
    {
        foreach (var statType in _statTextDict.Keys)
        {
            var value = PlayerStatsManager.Instance.GetStatValue(statType);
            _statTextDict[statType].text =
                $"{statTypeNames.GetStatTypeName(statType)}: {value:N0}";
        }
        statTextDictValues = _statTextDict.Values.ToList();
    }
}
