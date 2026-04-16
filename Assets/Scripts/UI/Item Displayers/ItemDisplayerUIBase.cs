using UnityEngine;

public abstract class ItemDisplayerUIBase : MonoBehaviour
{
    /// <summary>
    /// Shows the item's data. If it's null, clears the display.
    /// </summary>
    public abstract void Setup(ItemDataAsset item);
}
