using UnityEngine;
using UnityEngine.UI;

/// <summary>Displays the item's icon sprite.</summary>
public class ItemIconUI : ItemDisplayerUIBase
{
    [SerializeField] private Image iconImage;

    /// <summary>Assigns the item icon sprite to the Image component.</summary>
    public override void Setup(ItemDataAsset item)
    {
        iconImage.sprite = item.Icon;
    }
}
