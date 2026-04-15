using TMPro;
using UnityEngine;

/// <summary>Displays the item's name.</summary>
public class ItemNameUI : ItemDisplayerUIBase
{
    [SerializeField] private TMP_Text nameText;

    /// <summary>Displays the item name.</summary>
    public override void Setup(ItemDataAsset item)
    {
        nameText.text = item.ItemName;
    }
}
