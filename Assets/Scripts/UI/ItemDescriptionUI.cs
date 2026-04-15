using TMPro;
using UnityEngine;

/// <summary>Displays the item's description.</summary>
public class ItemDescriptionUI : ItemDisplayerUIBase
{
    [SerializeField] private TMP_Text descriptionText;

    /// <summary>Displays the item description.</summary>
    public override void Setup(ItemDataAsset item)
    {
        descriptionText.text = item.Description;
    }
}
