using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Root coordinator on the inventory row prefab. Delegates item data to each child display component.</summary>
public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private ItemDisplayerUIBase[] itemDisplayers;

    /// <summary>Delegates item data to each child display component.</summary>
    public void Setup(ItemDataAsset item)
    {
        foreach (var itemDisplayer in itemDisplayers)
        {
            itemDisplayer.Setup(item);
        }
    }

    private void Start()
    {
        print("Testing Inventory item ui");
        Setup(InventoryManager.Instance.GetItem(0));
    }
}
