using UnityEngine.UI;

public class ShopInventoryUI : InventoryUIBase
{
    private void OnValidate()
    {
        targetInventory = InventoryType.Shop;
    }

    protected override void AddActionButtonCallback(Button button, Gear gear)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(
            () => PlayerBuyingManager.Instance.TryBuy(gear));
    }
}