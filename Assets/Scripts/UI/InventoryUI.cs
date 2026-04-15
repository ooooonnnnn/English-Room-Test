using System.Collections.Generic;
using UnityEngine;

/// <summary>Populates the inventory scroll list and refreshes it when the inventory changes.</summary>
public class InventoryUI : MonoBehaviour
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

        List<ItemDataAsset> sortedItems = InventoryManager.Instance.GetSortedItems();
        foreach (ItemDataAsset itemData in sortedItems)
        {
            InventoryItemUI row = Instantiate(itemRowPrefab, itemContainer);
            row.Setup(itemData);
        }
    }

    private void ClearContainer()
    {
        var childrenToDestroy = new List<Transform>();
        foreach (Transform child in itemContainer)
            childrenToDestroy.Add(child);

        foreach (Transform child in childrenToDestroy)
            Destroy(child.gameObject);
    }
}
