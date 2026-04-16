using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Objects/Item")]
public class ItemDataAsset : ScriptableObject
{
    [Header("Functional Properties")]
    [SerializeField] private ItemType itemType;
    [SerializeField] private StatAffectorData[] statAffectorData;
    [SerializeField] private float price;
    
    [Header("Display Properties")]
    [SerializeField] private string itemName;
    [SerializeField] private string flavorDescription;
    [SerializeField] private Sprite icon;

    [SerializeField, HideInInspector] private string statEffectsDescription;

    /// <summary>
    /// The type of the item. You can only equip an item of a specific type in each slot.
    /// </summary>
    public ItemType ItemType => itemType;
    
    /// <summary>The display name of the item.</summary>
    public string ItemName => itemName;

    /// <summary>The price of the item.</summary>
    public float Price => price;

    /// <summary>A short description of the item.</summary>
    public string Description => string.Concat(flavorDescription, "\n", statEffectsDescription);

    /// <summary>The icon sprite representing the item.</summary>
    public Sprite Icon => icon;
    
    /// <summary>
    /// Array of stat effects this item has
    /// </summary>
    public StatAffectorData[] StatAffectorData => statAffectorData;

    private void OnValidate()
    {
        ConstructStatEffectDescription();
    }

    private void ConstructStatEffectDescription()
    {
        // var statNamesAssetGUIDS = AssetDatabase.FindAssets(nameof(StatType_Names));
        // if (statNamesAssetGUIDS.Length == 0)
        // {
        //     Debug.LogWarning("No statType names asset found, can't generate stat effects description");
        //     return;
        // }
        // var statNamesAssetPath = AssetDatabase.GUIDToAssetPath(statNamesAssetGUIDS[0]);
        // var statNamesAsset = AssetDatabase.LoadAssetAtPath<StatType_Names>(statNamesAssetPath);
        var statNamesAsset = StatType_Names.LoadSingletonAsset();
        
        var descriptions = new List<string>();
        foreach (var affectorData in StatAffectorData)
        {
            descriptions.Add($"{statNamesAsset.GetStatTypeName(affectorData.StatType)}: {affectorData.Value:+0;-0;+0}");
        }

        statEffectsDescription = string.Join(' ', descriptions);
    }
}
