using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Holds an item
/// </summary>
public class PlayerGearSlot : MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    public ItemType ItemType => itemType;
    
    public UnityEvent<Gear> onItemChanged;

    [SerializeField] [CanBeNull] private Gear currentItem;
    public Gear Item
    {
        get => currentItem;
        set
        {
            currentItem = value;
            currentItem?.transform.SetParent(transform);
            onItemChanged?.Invoke(currentItem);
        }
    }

    private void OnValidate()
    {
        ValidateItemType();
    }

    private void ValidateItemType()
    {
        if (currentItem == null) return;
        if (currentItem.ItemData.ItemType != itemType) currentItem = null;
    }
}
