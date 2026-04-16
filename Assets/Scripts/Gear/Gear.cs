using System;
using UnityEngine;

/// <summary>
/// Represents a specific item
/// </summary>
[Serializable]
public class Gear : MonoBehaviour
{
    public ItemDataAsset ItemData => itemData;
    
    [SerializeField] private ItemDataAsset itemData;
}
