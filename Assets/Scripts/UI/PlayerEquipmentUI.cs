using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerEquipmentUI : MonoBehaviour
{
    private Dictionary<PlayerGearSlot, InventoryItemUI> _equipmentSlots = new();
    [SerializeField] private List<PlayerGearSlot> equipmentSlotsDictKeys;
    [SerializeField] private List<InventoryItemUI> equipmentSlotsDictValues;

    private void OnValidate()
    {
        InitializeEquipmentSlots();
    }

    private void InitializeEquipmentSlots()
    {
        var gearSlots = FindObjectsByType<PlayerGearSlot>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        CreateEquipmentSlotsDictionary();
        foreach (var slot in gearSlots)
        {
            _equipmentSlots.TryAdd(slot, null);
        }
        equipmentSlotsDictKeys = _equipmentSlots.Keys.ToList();
        equipmentSlotsDictValues = _equipmentSlots.Values.ToList();
    }

    private void Awake()
    {
        CreateEquipmentSlotsDictionary();
    }

    private void CreateEquipmentSlotsDictionary()
    {
        if (equipmentSlotsDictKeys == null || equipmentSlotsDictValues == null)
        {
            _equipmentSlots = new ();
            equipmentSlotsDictKeys = new ();
            equipmentSlotsDictValues = new ();
        }
        else
        {
            _equipmentSlots = new Dictionary<PlayerGearSlot, InventoryItemUI>(
                equipmentSlotsDictKeys.Zip(equipmentSlotsDictValues,
                    (key, val) => new KeyValuePair<PlayerGearSlot, InventoryItemUI>(key, val)));
        }
    }

    private void Start()
    {
        SetUnequipCallbacks();
    }

    private void SetUnequipCallbacks()
    {
        if (!PlayerGearManager.Instance)
        {
            Debug.LogError("PlayerGearManager is not initialized");
            return;
        }
        
        foreach (var slotUIPair in _equipmentSlots)
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
        foreach (var slotUIPair in _equipmentSlots)
        {
            var gearEquippedInSlot = slotUIPair.Key.Item;
            var slotUI = slotUIPair.Value;
            slotUI.DisplayItemData(gearEquippedInSlot ? gearEquippedInSlot.ItemData : null);
        }
    }
}
