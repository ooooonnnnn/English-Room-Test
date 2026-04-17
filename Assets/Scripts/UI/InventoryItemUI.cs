using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Root coordinator on the inventory row prefab. Delegates item data to each child display component.</summary>
public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private ItemDisplayerUIBase[] itemDisplayers;
    [SerializeField] private Button actionButton;
    public Button ActionButton => actionButton;

    #if UNITY_EDITOR
    private void OnValidate()
    {
        itemDisplayers = GetComponentsInChildren<ItemDisplayerUIBase>();
    }
    #endif

    /// <summary>Delegates item data to each child display component.</summary>
    public void DisplayItemData(ItemDataAsset item)
    {
        foreach (var itemDisplayer in itemDisplayers)
        {
            itemDisplayer.Setup(item);
        }
    }
}
