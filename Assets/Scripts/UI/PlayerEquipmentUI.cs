using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerEquipmentUI : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<PlayerGearSlot, InventoryItemUI> equipmentSlots;

    private void OnValidate()
    {
        InitializeEquipmentSlots();

    }

    private void Start()
    {
        SetUnequipCallbacks();
    }

    private void InitializeEquipmentSlots()
    {
        var gearSlots = FindObjectsByType<PlayerGearSlot>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var slot in gearSlots)
        {
            equipmentSlots.TryAdd(slot, null);
        }
    }

    private void SetUnequipCallbacks()
    {
        if (!PlayerGearManager.Instance)
        {
            Debug.LogError("PlayerGearManager is not initialized");
            return;
        }
        
        foreach (var slotUIPair in equipmentSlots)
        {
            var slotUI = slotUIPair.Value;
            if (!slotUI) continue;
            
            var unequipCallback = slotUI.ActionButton.onClick;
            unequipCallback.RemoveAllListeners();
            unequipCallback.AddListener(slotUIPair.Key.TryUnequip);
        }
    }

    public void Refresh()
    {
        foreach (var slotUIPair in equipmentSlots)
        {
            var gearEquippedInSlot = slotUIPair.Key.Item;
            var slotUI = slotUIPair.Value;
            slotUI.DisplayItemData(gearEquippedInSlot ? gearEquippedInSlot.ItemData : null);
        }
    }
}
