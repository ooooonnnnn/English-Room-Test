using UnityEngine;

public abstract class ItemDisplayerUIBase : MonoBehaviour
{
    /// <summary>
    /// Shows the item's data.
    /// </summary>
    public abstract void Setup(ItemDataAsset item);
}
