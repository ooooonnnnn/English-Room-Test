using TMPro;
using UnityEngine;

/// <summary>Displays the item's price.</summary>
public class ItemPriceUI : ItemDisplayerUIBase
{
    [SerializeField] private TMP_Text priceText;

    /// <summary>Displays the formatted item price.</summary>
    public override void Setup(ItemDataAsset item)
    {
        priceText.text = item.Price.ToString("N0");
    }
}
