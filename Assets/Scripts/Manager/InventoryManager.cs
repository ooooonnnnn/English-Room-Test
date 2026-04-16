using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Gives access to all inventories
/// </summary>
public class InventoryManager : PersistentSingleton<InventoryManager>
{
    [SerializeField] private SerializedDictionary<InventoryType, Inventory> inventories;
    
    public Inventory GetInventory(InventoryType inventoryType) => inventories.GetValueOrDefault(inventoryType,null);

    private void OnValidate()
    {
        inventories = new();
        foreach (var inventory in FindObjectsByType<Inventory>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            inventories[inventory.InventoryType] = inventory;
        }
    }
}
