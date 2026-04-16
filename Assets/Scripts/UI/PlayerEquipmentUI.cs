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

    private void InitializeEquipmentSlots()
    {
        var gearSlots = FindObjectsByType<PlayerGearSlot>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var slot in gearSlots)
        {
            equipmentSlots.TryAdd(slot, null);
        }
    }

    public void Refresh()
    {
        foreach (var slotUIPair in equipmentSlots)
        {
            var gearEquippedInSlot = slotUIPair.Key.Item;
            if (!gearEquippedInSlot) continue;
            
            var slotUI = slotUIPair.Value;
            slotUI.Setup(gearEquippedInSlot.ItemData);
            var unequipCallback = slotUI.ActionButton.onClick;
            unequipCallback.RemoveAllListeners();
            //TODO: Add unequip
        }
    }
}
