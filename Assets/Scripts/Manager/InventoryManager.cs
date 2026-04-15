using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>Singleton manager that owns the player inventory — a sorted list of ItemData.</summary>
public class InventoryManager : PersistentSingleton<InventoryManager>
{
    [SerializeField] private List<ItemDataAsset> items;

    public UnityEvent onInventoryChanged;

    /// <summary>Returns a new list of items sorted by Price ascending.</summary>
    public List<ItemDataAsset> GetSortedItems()
    {
        var sorted = new List<ItemDataAsset>(items);
        sorted.Sort((a, b) => a.Price.CompareTo(b.Price));
        return sorted;
    }

    /// <summary>Adds an item to the inventory and fires onInventoryChanged.</summary>
    public void AddItem(ItemDataAsset item)
    {
        items.Add(item);
        onInventoryChanged?.Invoke();
    }

    /// <summary>Removes the first occurrence of the item and fires onInventoryChanged.</summary>
    public void RemoveItem(ItemDataAsset item)
    {
        items.Remove(item);
        onInventoryChanged?.Invoke();
    }
    
    public ItemDataAsset GetItem(int index) => items[index];
}
