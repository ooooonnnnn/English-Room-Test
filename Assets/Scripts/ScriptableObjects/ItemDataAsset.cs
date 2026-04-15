using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Objects/Item")]
public class ItemDataAsset : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private float price;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    /// <summary>The display name of the item.</summary>
    public string ItemName => itemName;

    /// <summary>The price of the item.</summary>
    public float Price => price;

    /// <summary>A short description of the item.</summary>
    public string Description => description;

    /// <summary>The icon sprite representing the item.</summary>
    public Sprite Icon => icon;
}
