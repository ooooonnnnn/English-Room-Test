using UnityEngine;

/// <summary>
/// Can buy items from the shop ana place them in the inventory
/// </summary>
public class PlayerBuyingManager : PersistentSingleton<PlayerBuyingManager>
{
    public void TryBuy(Gear itemToBuy)
    {
        if (!PlayerWalletManager.Instance || !InventoryManager.Instance)
        {
            Debug.LogError("PlayerWalletManager or InventoryManager not found");
            return;
        }
        
        if (!PlayerWalletManager.Instance.CanBuyItem(itemToBuy))
        {
            print("Not enough money to buy");
            return;
        }
        
        PlayerWalletManager.Instance.AddMoney(-itemToBuy.ItemData.Price);
        InventoryManager.Instance.GetInventory(InventoryType.Shop)?.RemoveItem(itemToBuy);
        InventoryManager.Instance.GetInventory(InventoryType.Player)?.AddItem(itemToBuy);
    }
}
