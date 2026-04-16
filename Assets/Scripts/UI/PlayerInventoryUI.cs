using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : InventoryUIBase
{
    private void OnValidate()
    {
        targetInventory = InventoryType.Player;
    }

    protected override void AddActionButtonCallback(Button button, Gear gear)
    {
        if (!PlayerGearManager.Instance) Debug.LogError("PlayerGearManager is not initialized!");
        button.onClick.AddListener(() => PlayerGearManager.Instance.TryEquip(gear));
    }
}
