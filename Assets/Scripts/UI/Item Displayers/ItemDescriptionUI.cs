using System;
using TMPro;
using UnityEngine;

/// <summary>Displays the item's description.</summary>
[RequireComponent(typeof(TMP_Text))]
public class ItemDescriptionUI : ItemDisplayerUIBase
{
    [SerializeField] private TMP_Text descriptionText;

    private void OnValidate()
    {
        descriptionText = GetComponent<TMP_Text>();
    }

    /// <summary>Displays the item description.</summary>
    public override void Setup(ItemDataAsset item)
    {
        descriptionText.text = item.Description;
    }
}
