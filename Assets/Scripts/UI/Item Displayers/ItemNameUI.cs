using System;
using TMPro;
using UnityEngine;

/// <summary>Displays the item's name.</summary>
[RequireComponent(typeof(TMP_Text))]
public class ItemNameUI : ItemDisplayerUIBase
{
    [SerializeField] private TMP_Text nameText;

    private void OnValidate()
    {
        nameText = GetComponent<TMP_Text>();
    }

    /// <summary>Displays the item name.</summary>
    public override void Setup(ItemDataAsset item)
    {
        nameText.text = item.ItemName;
    }
}
