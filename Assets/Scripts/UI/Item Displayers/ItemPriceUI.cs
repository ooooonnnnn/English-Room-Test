using System;
using TMPro;
using UnityEngine;

/// <summary>Displays the item's price.</summary>
[RequireComponent(typeof(TMP_Text))]
public class ItemPriceUI : ItemDisplayerUIBase
{
    [SerializeField] private TMP_Text priceText;

    private void OnValidate()
    {
        priceText = GetComponent<TMP_Text>();
    }

    /// <summary>Displays the formatted item price.</summary>
    public override void Setup(ItemDataAsset item)
    {
        priceText.text = item ? $"{item.Price:N0} g" : "";
    }
}
