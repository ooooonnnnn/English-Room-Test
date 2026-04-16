using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Populates the inventory scroll list and refreshes it when commanded to .</summary>
public abstract class InventoryUIBase : MonoBehaviour
{
    [SerializeField] private Transform itemContainer;
    [SerializeField] private InventoryItemUI itemRowPrefab;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        ClearContainer();

        PopulateContainer();
    }

    private void ClearContainer()
    {
        var childrenToDestroy = new List<Transform>();
        foreach (Transform child in itemContainer)
            childrenToDestroy.Add(child);

        foreach (Transform child in childrenToDestroy)
            Destroy(child.gameObject);
    }

    private void PopulateContainer()
    {
        List<Gear> sortedItems = InventoryManager.Instance.GetSortedItems();
        foreach (Gear gear in sortedItems)
        {
            InventoryItemUI row = Instantiate(itemRowPrefab, itemContainer);
            row.Setup(gear.ItemData);
            
            SafeAddActionButtonCallback(row, gear);
        }
    }

    /// <summary>
    /// Adds a listener to the action button of the given inventory item.
    /// </summary>
    private void SafeAddActionButtonCallback(InventoryItemUI itemUI, Gear gear)
    {
        if (!itemUI.ActionButton)
        {
            Debug.LogError($"Inventory item UI {itemUI.name} has no action button!");
            return;
        }
        AddActionButtonCallback(itemUI.ActionButton, gear);
    }

    protected abstract void AddActionButtonCallback(Button button, Gear gear);
}
