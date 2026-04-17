using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Displays the item's icon sprite.</summary>
[RequireComponent(typeof(Image))]
public class ItemIconUI : ItemDisplayerUIBase
{
    [SerializeField] private Image iconImage;

    #if UNITY_EDITOR
    private void OnValidate()
    {
        iconImage = GetComponent<Image>();
    }
    #endif

    /// <summary>Assigns the item icon sprite to the Image component.</summary>
    public override void Setup(ItemDataAsset item)
    {
        iconImage.sprite = item ? item.Icon : null;
    }
}
