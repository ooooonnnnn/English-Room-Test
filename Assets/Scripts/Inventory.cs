using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>Singleton manager that owns the player inventory — a sorted list of ItemData.</summary>
public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryType inventoryType;
    public InventoryType InventoryType => inventoryType;
    [SerializeField] private List<Gear> items;

    public UnityEvent onInventoryChanged;

    /// <summary>Returns a new list of items sorted by Price ascending.</summary>
    public List<Gear> GetSortedItems()
    {
        var sorted = new List<Gear>(items);
        sorted.Sort((a, b) => a.ItemData.Price.CompareTo(b.ItemData.Price));
        return sorted;
    }

    /// <summary>Adds an item to the inventory and fires onInventoryChanged.</summary>
    public void AddItem(Gear item)
    {
        items.Add(item);
        onInventoryChanged?.Invoke();
    }

    /// <summary>Removes the first occurrence of the item and fires onInventoryChanged.</summary>
    public void RemoveItem(Gear item)
    {
        items.Remove(item);
        onInventoryChanged?.Invoke();
    }
}
