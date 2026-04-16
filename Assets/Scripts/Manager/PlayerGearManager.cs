using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class PlayerGearManager : PersistentSingleton<PlayerGearManager>
{
    [SerializeField] private SerializedDictionary<ItemType, List<PlayerGearSlot>> gearSlots;
    public UnityEvent onGearChanged;

    #region Initialization

    private void OnValidate()
    {
        InitializeGearSlots();
    }

    private void InitializeGearSlots()
    {
        gearSlots = new ();

        foreach (Transform child in transform)
        {
            if (!child.TryGetComponent(out PlayerGearSlot slot)) continue;

            gearSlots.TryAdd(slot.ItemType, new List<PlayerGearSlot>());
            
            gearSlots[slot.ItemType].Add(slot);
        }
    }

    #endregion

    public void TryEquip(Gear item)
    {
        var itemType = item.ItemData.ItemType;
        if (!gearSlots.ContainsKey(itemType))
        {
            Debug.LogWarning($"No gear slot found for item type {itemType}");
            return;
        }

        //Find the first empty slot
        var emptySlot = gearSlots[itemType].Find(slot => slot.Item == null);
        if (!emptySlot)
        {
            print($"No empty slot to equip {item.ItemData.ItemName}");
            return;
        }
        
        //Equip the item
        emptySlot.Item = item;
        onGearChanged.Invoke();
        print($"Equipped {item.ItemData.ItemName} to slot {emptySlot.name}");
        InventoryManager.Instance?.GetInventory(InventoryType.Player)?.RemoveItem(item);
    }

    public List<Gear> GetEquippedGear()
    {
        var equippedGear = new List<Gear>();
        foreach (var slotList in gearSlots.Values)
        {
            foreach (var slot in slotList)
            {
                if (slot.Item)
                    equippedGear.Add(slot.Item);
            }
        }

        return equippedGear;
    }
}
