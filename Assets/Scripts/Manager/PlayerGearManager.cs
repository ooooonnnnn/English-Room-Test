using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGearManager : PersistentSingleton<PlayerGearManager>
{
    private Dictionary<ItemType, SerializableList<PlayerGearSlot>> _gearSlots;
    [SerializeField] private List<ItemType> gearSlotsDictKeys;
    [SerializeField] private List<SerializableList<PlayerGearSlot>> gearSlotsDictValues;
    public UnityEvent onGearChanged;

    #region Initialization

    protected override void Awake()
    {
        base.Awake();
        _gearSlots = new (
            gearSlotsDictKeys.Zip(gearSlotsDictValues, (key, val) 
                => new KeyValuePair<ItemType, SerializableList<PlayerGearSlot>>(key, val)));
    }

    private void OnValidate()
    {
        InitializeGearSlots();
    }

    private void InitializeGearSlots()
    {
        _gearSlots = new ();

        foreach (Transform child in transform)
        {
            if (!child.TryGetComponent(out PlayerGearSlot slot)) continue;

            _gearSlots.TryAdd(slot.ItemType, new SerializableList<PlayerGearSlot>());
            
            _gearSlots[slot.ItemType].Add(slot);
        }
        
        gearSlotsDictKeys = _gearSlots.Keys.ToList();
        gearSlotsDictValues = _gearSlots.Values.ToList();
    }

    #endregion

    public void TryEquip(Gear item)
    {
        var itemType = item.ItemData.ItemType;
        if (!_gearSlots.ContainsKey(itemType))
        {
            Debug.LogWarning($"No gear slot found for item type {itemType}");
            return;
        }

        //Find the first empty slot
        var emptySlot = _gearSlots[itemType].Find(slot => slot.Item == null);
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

    public void TryUnequipFromSlot(PlayerGearSlot slot)
    {
        if (!slot.Item)
        {
            print("Nothing to unequip");
            return;
        }
        
        var item = slot.Item;
        slot.Item = null;
        onGearChanged.Invoke();
        InventoryManager.Instance?.GetInventory(InventoryType.Player)?.AddItem(item);
        print($"Unequipped {item.ItemData.ItemName} from slot {slot.name}");
    }

    public List<Gear> GetEquippedGear()
    {
        var equippedGear = new List<Gear>();
        foreach (var slotList in _gearSlots.Values)
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
